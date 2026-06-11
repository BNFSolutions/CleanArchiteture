import { clienteApi } from "../api/clienteApi";
import CrudPanel, { formatCpf, stripCpfDigits } from "./CrudPanel";

const EMPTY_FORM = {
  nomeDto: "",
  cpfDto: ""
};

export default function ClientesPanel() {
  return (
    <CrudPanel
      title="Clientes cadastrados"
      resourceLabel="cliente"
      icon="users"
      description="Informe nome e CPF do cliente. O ID e gerado automaticamente."
      api={clienteApi}
      emptyForm={EMPTY_FORM}
      fields={[
        { name: "nomeDto", label: "Nome", required: true, maxLength: 100 },
        { name: "cpfDto", label: "CPF", type: "cpf", required: true }
      ]}
      columns={[
        { key: "nomeDto", label: "Nome" },
        { key: "cpfDto", label: "CPF", format: (value) => formatCpf(value) }
      ]}
      mapRow={(item) => ({
        nomeDto: item.nomeDto ?? "",
        cpfDto: formatCpf(item.cpfDto ?? "")
      })}
      buildCreatePayload={(form) => ({
        nomeDto: form.nomeDto,
        cpfDto: stripCpfDigits(form.cpfDto)
      })}
      buildUpdatePayload={(form) => ({
        nomeDto: form.nomeDto,
        cpfDto: stripCpfDigits(form.cpfDto)
      })}
    />
  );
}
