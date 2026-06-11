import { useEffect, useMemo, useState } from "react";
import { categoriaApi } from "../api/categoriaApi";
import { produtoApi } from "../api/produtoApi";
import CrudPanel, { formatMoney } from "./CrudPanel";

const EMPTY_FORM = {
  codProdutoDto: "",
  descProdutoDto: "",
  vlrPrecoDto: "",
  categoriaIdDto: ""
};

function formatCategoriaLabel(categoria) {
  return `${categoria.codCategoriaDto} - ${categoria.descCategoriaDto}`;
}

export default function ProdutosPanel() {
  const [categorias, setCategorias] = useState([]);
  const [loadingCategorias, setLoadingCategorias] = useState(true);

  useEffect(() => {
    let cancelled = false;

    async function loadCategorias() {
      setLoadingCategorias(true);
      try {
        const data = await categoriaApi.getAllAsync();
        if (!cancelled) {
          setCategorias(Array.isArray(data) ? data : []);
        }
      } catch {
        if (!cancelled) {
          setCategorias([]);
        }
      } finally {
        if (!cancelled) {
          setLoadingCategorias(false);
        }
      }
    }

    loadCategorias();
    return () => {
      cancelled = true;
    };
  }, []);

  const categoriaOptions = useMemo(
    () =>
      categorias.map((categoria) => ({
        value: String(categoria.idDto),
        label: formatCategoriaLabel(categoria)
      })),
    [categorias]
  );

  const categoriaPorId = useMemo(
    () => new Map(categorias.map((categoria) => [categoria.idDto, categoria])),
    [categorias]
  );

  return (
    <CrudPanel
      title="Produtos cadastrados"
      resourceLabel="produto"
      icon="products"
      description="Informe os dados e selecione a categoria. O ID e gerado automaticamente."
      api={produtoApi}
      emptyForm={EMPTY_FORM}
      fields={[
        { name: "codProdutoDto", label: "Codigo", required: true, maxLength: 30 },
        { name: "descProdutoDto", label: "Descricao", required: true, maxLength: 100 },
        { name: "vlrPrecoDto", label: "Preco", type: "number", step: "0.01", min: "0", required: true },
        {
          name: "categoriaIdDto",
          label: "Categoria",
          type: "select",
          required: true,
          disabled: loadingCategorias || categoriaOptions.length === 0,
          placeholder: loadingCategorias
            ? "Carregando categorias..."
            : categoriaOptions.length === 0
              ? "Nenhuma categoria cadastrada"
              : "Selecione uma categoria",
          options: categoriaOptions
        }
      ]}
      columns={[
        { key: "codProdutoDto", label: "Codigo" },
        { key: "descProdutoDto", label: "Produto" },
        { key: "vlrPrecoDto", label: "Preco", format: (value) => formatMoney(value) },
        {
          key: "categoriaIdDto",
          label: "Categoria",
          format: (value) => {
            const categoria = categoriaPorId.get(value);
            return categoria ? formatCategoriaLabel(categoria) : value;
          }
        }
      ]}
      mapRow={(item) => ({
        codProdutoDto: item.codProdutoDto ?? "",
        descProdutoDto: item.descProdutoDto ?? "",
        vlrPrecoDto: String(item.vlrPrecoDto ?? ""),
        categoriaIdDto: String(item.categoriaIdDto ?? "")
      })}
      buildCreatePayload={(form) => ({
        codProdutoDto: form.codProdutoDto,
        descProdutoDto: form.descProdutoDto,
        vlrPrecoDto: Number(form.vlrPrecoDto),
        categoriaIdDto: Number(form.categoriaIdDto)
      })}
      buildUpdatePayload={(form) => ({
        codProdutoDto: form.codProdutoDto,
        descProdutoDto: form.descProdutoDto,
        vlrPrecoDto: Number(form.vlrPrecoDto),
        categoriaIdDto: Number(form.categoriaIdDto)
      })}
    />
  );
}
