import { useEffect, useMemo, useState } from "react";
import { clienteApi } from "../api/clienteApi";
import { compraApi } from "../api/compraApi";
import CrudPanel, { formatCpf } from "./CrudPanel";

function nowLocal() {
  const d = new Date();
  const pad = (n) => String(n).padStart(2, "0");
  return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}T${pad(d.getHours())}:${pad(d.getMinutes())}`;
}

function formatDate(value) {
  if (!value) {
    return "";
  }
  const date = new Date(value);
  return Number.isNaN(date.getTime()) ? value : date.toLocaleString("pt-BR");
}

const EMPTY_FORM = {
  clienteIdDto: "",
  dataCompraDto: nowLocal()
};

export default function ComprasPanel() {
  const [clientes, setClientes] = useState([]);
  const [loadingClientes, setLoadingClientes] = useState(true);

  useEffect(() => {
    let cancelled = false;

    async function loadClientes() {
      setLoadingClientes(true);
      try {
        const data = await clienteApi.getAllAsync();
        if (!cancelled) {
          setClientes(Array.isArray(data) ? data : []);
        }
      } catch {
        if (!cancelled) {
          setClientes([]);
        }
      } finally {
        if (!cancelled) {
          setLoadingClientes(false);
        }
      }
    }

    loadClientes();
    return () => {
      cancelled = true;
    };
  }, []);

  const clienteOptions = useMemo(
    () =>
      clientes.map((cliente) => ({
        value: String(cliente.idDto),
        label: `${formatCpf(cliente.cpfDto)} - ${cliente.nomeDto}`
      })),
    [clientes]
  );

  const clientePorId = useMemo(
    () => new Map(clientes.map((cliente) => [cliente.idDto, cliente])),
    [clientes]
  );

  return (
    <CrudPanel
      title="Compras cadastradas"
      resourceLabel="compra"
      icon="cart"
      description="Selecione o cliente e a data. O ID e gerado automaticamente."
      api={compraApi}
      emptyForm={EMPTY_FORM}
      fields={[
        {
          name: "clienteIdDto",
          label: "Cliente",
          type: "select",
          required: true,
          disabled: loadingClientes || clienteOptions.length === 0,
          placeholder: loadingClientes
            ? "Carregando clientes..."
            : clienteOptions.length === 0
              ? "Nenhum cliente cadastrado"
              : "Selecione um cliente",
          options: clienteOptions
        },
        {
          name: "dataCompraDto",
          label: "Data da compra",
          type: "datetime-local",
          required: true
        }
      ]}
      columns={[
        {
          key: "clienteIdDto",
          label: "Cliente",
          format: (value) => {
            const cliente = clientePorId.get(value);
            return cliente ? `${formatCpf(cliente.cpfDto)} - ${cliente.nomeDto}` : value;
          }
        },
        { key: "dataCompraDto", label: "Data", format: (value) => formatDate(value) }
      ]}
      mapRow={(item) => ({
        clienteIdDto: String(item.clienteIdDto ?? ""),
        dataCompraDto: item.dataCompraDto ? String(item.dataCompraDto).slice(0, 16) : nowLocal()
      })}
      buildCreatePayload={(form) => ({
        clienteIdDto: Number(form.clienteIdDto),
        dataCompraDto: form.dataCompraDto || null
      })}
      buildUpdatePayload={(form) => ({
        clienteIdDto: Number(form.clienteIdDto),
        dataCompraDto: form.dataCompraDto || null
      })}
    />
  );
}
