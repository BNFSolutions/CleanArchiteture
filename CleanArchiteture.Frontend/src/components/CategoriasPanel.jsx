import { categoriaApi } from "../api/categoriaApi";
import CrudPanel from "./CrudPanel";

const EMPTY_FORM = {
  codCategoriaDto: "",
  descCategoriaDto: ""
};

export default function CategoriasPanel() {
  return (
    <CrudPanel
      title="Categorias cadastradas"
      resourceLabel="categoria"
      icon="categories"
      description="Defina codigo e descricao. O ID e gerado automaticamente."
      api={categoriaApi}
      emptyForm={EMPTY_FORM}
      fields={[
        { name: "codCategoriaDto", label: "Codigo", required: true, maxLength: 30 },
        { name: "descCategoriaDto", label: "Descricao", required: true, maxLength: 100 }
      ]}
      columns={[
        { key: "codCategoriaDto", label: "Codigo" },
        { key: "descCategoriaDto", label: "Descricao" }
      ]}
      mapRow={(item) => ({
        codCategoriaDto: item.codCategoriaDto ?? "",
        descCategoriaDto: item.descCategoriaDto ?? ""
      })}
      buildCreatePayload={(form) => ({
        codCategoriaDto: form.codCategoriaDto,
        descCategoriaDto: form.descCategoriaDto
      })}
      buildUpdatePayload={(form) => ({
        codCategoriaDto: form.codCategoriaDto,
        descCategoriaDto: form.descCategoriaDto
      })}
    />
  );
}
