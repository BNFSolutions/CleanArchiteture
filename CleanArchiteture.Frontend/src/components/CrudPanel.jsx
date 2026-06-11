import { useCallback, useEffect, useState } from "react";
import Icon from "./Icon";

function formatMoney(value) {
  return new Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(value ?? 0);
}

function stripCpfDigits(value) {
  return String(value ?? "").replace(/\D/g, "").slice(0, 11);
}

function formatCpf(value) {
  const digits = stripCpfDigits(value);
  if (digits.length <= 3) return digits;
  if (digits.length <= 6) return `${digits.slice(0, 3)}.${digits.slice(3)}`;
  if (digits.length <= 9) return `${digits.slice(0, 3)}.${digits.slice(3, 6)}.${digits.slice(6)}`;
  return `${digits.slice(0, 3)}.${digits.slice(3, 6)}.${digits.slice(6, 9)}-${digits.slice(9)}`;
}

export default function CrudPanel({
  title,
  resourceLabel,
  description,
  icon = "products",
  api,
  fields,
  columns,
  emptyForm,
  mapRow,
  buildCreatePayload,
  buildUpdatePayload
}) {
  const [items, setItems] = useState([]);
  const [form, setForm] = useState(emptyForm);
  const [editingId, setEditingId] = useState(null);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState("");
  const [lookupId, setLookupId] = useState("");
  const [existsResult, setExistsResult] = useState(null);
  const [successMessage, setSuccessMessage] = useState("");
  const [filter, setFilter] = useState("");

  const loadItems = useCallback(async () => {
    setLoading(true);
    setError("");
    try {
      const data = await api.getAllAsync();
      setItems(Array.isArray(data) ? data : []);
    } catch (err) {
      setError(err.message || "Falha ao carregar registros.");
      setItems([]);
    } finally {
      setLoading(false);
    }
  }, [api]);

  useEffect(() => {
    loadItems();
  }, [loadItems]);

  function resetForm() {
    setForm(emptyForm);
    setEditingId(null);
    setSuccessMessage("");
  }

  async function handleSubmit(event) {
    event.preventDefault();
    setSaving(true);
    setError("");
    setSuccessMessage("");
    try {
      if (editingId) {
        const id = editingId;
        await api.updateAsync(editingId, buildUpdatePayload(form));
        resetForm();
        setSuccessMessage(`${resourceLabel} #${id} atualizado com sucesso.`);
      } else {
        const created = await api.addAsync(buildCreatePayload(form));
        const newId = created?.idDto ?? created?.id ?? created?.Id;
        resetForm();
        setSuccessMessage(
          newId
            ? `${resourceLabel} criado com ID ${newId} (gerado automaticamente).`
            : `${resourceLabel} criado com sucesso.`
        );
      }
      await loadItems();
    } catch (err) {
      setError(err.message || "Falha ao salvar registro.");
    } finally {
      setSaving(false);
    }
  }

  function handleEdit(item) {
    setError("");
    setSuccessMessage("");
    setEditingId(item.idDto);
    setForm(mapRow(item));
    if (typeof window !== "undefined") {
      window.scrollTo({ top: 0, behavior: "smooth" });
    }
  }

  async function handleDelete(id) {
    if (!window.confirm(`Excluir ${resourceLabel} #${id}?`)) {
      return;
    }

    setError("");
    setSuccessMessage("");
    try {
      const exists = await api.existsAsync(id);
      if (!exists) {
        setError("Registro nao encontrado para exclusao.");
        return;
      }
      await api.deleteAsync(id);
      if (editingId === id) {
        resetForm();
      }
      setSuccessMessage(`${resourceLabel} #${id} excluido.`);
      await loadItems();
    } catch (err) {
      setError(err.message || "Falha ao excluir registro.");
    }
  }

  async function handleLookup() {
    const id = Number(lookupId);
    if (!id) {
      setError("Informe um ID valido para consulta.");
      return;
    }

    setError("");
    setSuccessMessage("");
    try {
      const exists = await api.existsAsync(id);
      setExistsResult(exists);
      if (!exists) {
        return;
      }
      const item = await api.getByIdAsync(id);
      setEditingId(item.idDto);
      setForm(mapRow(item));
    } catch (err) {
      setError(err.message || "Falha ao consultar registro.");
      setExistsResult(null);
    }
  }

  const filterTerm = filter.trim().toLowerCase();
  const filteredItems = filterTerm
    ? items.filter((item) =>
        columns.some((column) => {
          const value = item[column.key];
          return value != null && String(value).toLowerCase().includes(filterTerm);
        })
      )
    : items;

  return (
    <div className="stack">
      <section className="card">
        <header className="card-head">
          <div className="card-head-title">
            <div className="card-head-icon">
              <Icon name={icon} size={20} />
            </div>
            <div>
              <h3>{editingId ? `Editar ${resourceLabel}` : `Novo ${resourceLabel}`}</h3>
              <p className="muted">
                {editingId
                  ? `Alterando o registro #${editingId} — ID gerado pelo banco`
                  : description ?? "Preencha os campos. O ID e gerado automaticamente."}
              </p>
            </div>
          </div>
          {editingId && (
            <button type="button" className="btn btn-ghost" onClick={resetForm}>
              <Icon name="plus" size={16} /> Novo
            </button>
          )}
        </header>

        <form className="crud-form" onSubmit={handleSubmit}>
          <div className="form-grid">
            {fields.map((field) => (
              <label key={field.name} className="field">
                <span>
                  {field.label}
                  {field.maxLength && field.type !== "select" && (
                    <em className="field-count">
                      {(form[field.name] ?? "").length}/{field.maxLength}
                    </em>
                  )}
                </span>
                {field.type === "select" ? (
                  <select
                    required={field.required}
                    disabled={field.disabled}
                    value={form[field.name] ?? ""}
                    onChange={(event) =>
                      setForm((prev) => ({
                        ...prev,
                        [field.name]: event.target.value
                      }))
                    }
                  >
                    <option value="">{field.placeholder ?? "Selecione..."}</option>
                    {(field.options ?? []).map((option) => (
                      <option key={option.value} value={option.value}>
                        {option.label}
                      </option>
                    ))}
                  </select>
                ) : field.type === "cpf" ? (
                  <input
                    type="text"
                    inputMode="numeric"
                    required={field.required}
                    value={form[field.name] ?? ""}
                    placeholder={field.placeholder ?? "000.000.000-00"}
                    onChange={(event) =>
                      setForm((prev) => ({
                        ...prev,
                        [field.name]: formatCpf(event.target.value)
                      }))
                    }
                  />
                ) : (
                  <input
                    type={field.type ?? "text"}
                    step={field.step}
                    min={field.min}
                    maxLength={field.maxLength}
                    required={field.required}
                    value={form[field.name] ?? ""}
                    placeholder={field.placeholder}
                    onChange={(event) =>
                      setForm((prev) => ({
                        ...prev,
                        [field.name]: event.target.value
                      }))
                    }
                  />
                )}
              </label>
            ))}
          </div>

          <div className="crud-actions">
            <button type="submit" className="btn btn-primary" disabled={saving}>
              {saving ? "Salvando..." : editingId ? "Salvar alteracoes" : `Criar ${resourceLabel}`}
            </button>
            {editingId && (
              <button type="button" className="btn btn-ghost" onClick={resetForm}>
                Cancelar
              </button>
            )}
          </div>

          {successMessage && (
            <p className="alert alert-success">
              <Icon name="check" size={16} /> {successMessage}
            </p>
          )}
          {error && (
            <p className="alert alert-error">
              <Icon name="alert" size={16} /> {error}
            </p>
          )}
        </form>
      </section>

      <section className="card">
        <header className="card-head">
          <div>
            <h3>{title}</h3>
            <p className="muted">{loading ? "Carregando..." : `${filteredItems.length} registro(s)`}</p>
          </div>
          <div className="table-tools">
            <div className="search compact">
              <Icon name="search" size={15} />
              <input
                type="text"
                placeholder="Filtrar..."
                value={filter}
                onChange={(event) => setFilter(event.target.value)}
              />
            </div>
            <div className="lookup">
              <input
                type="number"
                min="1"
                value={lookupId}
                onChange={(event) => setLookupId(event.target.value)}
                placeholder="ID"
              />
              <button type="button" className="btn btn-ghost" onClick={handleLookup}>
                Consultar
              </button>
            </div>
            {existsResult !== null && (
              <span className={existsResult ? "tag tag-ok" : "tag tag-warn"}>
                {existsResult ? "Existe" : "Nao existe"}
              </span>
            )}
          </div>
        </header>

        <div className="table-wrap">
          <table className="data-table">
            <thead>
              <tr>
                {columns.map((column) => (
                  <th key={column.key}>{column.label}</th>
                ))}
                <th className="col-actions">Acoes</th>
              </tr>
            </thead>
            <tbody>
              {loading ? (
                <tr>
                  <td colSpan={columns.length + 1} className="table-empty">
                    Carregando registros...
                  </td>
                </tr>
              ) : filteredItems.length === 0 ? (
                <tr>
                  <td colSpan={columns.length + 1} className="table-empty">
                    Nenhum registro encontrado.
                  </td>
                </tr>
              ) : (
                filteredItems.map((item) => (
                  <tr key={item.idDto}>
                    {columns.map((column) => (
                      <td key={column.key} data-label={column.label}>
                        {column.format ? column.format(item[column.key], item) : item[column.key]}
                      </td>
                    ))}
                    <td className="col-actions">
                      <div className="row-actions">
                        <button
                          type="button"
                          className="icon-btn"
                          onClick={() => handleEdit(item)}
                          aria-label="Editar"
                          title="Editar"
                        >
                          <Icon name="edit" size={16} />
                        </button>
                        <button
                          type="button"
                          className="icon-btn danger"
                          onClick={() => handleDelete(item.idDto)}
                          aria-label="Excluir"
                          title="Excluir"
                        >
                          <Icon name="trash" size={16} />
                        </button>
                      </div>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </section>
    </div>
  );
}

export { formatMoney, formatCpf, stripCpfDigits };
