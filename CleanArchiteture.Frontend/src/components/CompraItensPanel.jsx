import { useEffect, useMemo, useState } from "react";
import { compraApi } from "../api/compraApi";
import { compraItemApi } from "../api/compraItemApi";
import { produtoApi } from "../api/produtoApi";
import CrudPanel from "./CrudPanel";

const EMPTY_FORM = {
  compraIdDto: "",
  produtoIdDto: "",
  quantidadeDto: "1"
};

export default function CompraItensPanel() {
  const [compras, setCompras] = useState([]);
  const [produtos, setProdutos] = useState([]);
  const [loadingRefs, setLoadingRefs] = useState(true);

  useEffect(() => {
    let cancelled = false;

    async function loadRefs() {
      setLoadingRefs(true);
      try {
        const [comprasData, produtosData] = await Promise.all([
          compraApi.getAllAsync(),
          produtoApi.getAllAsync()
        ]);
        if (!cancelled) {
          setCompras(Array.isArray(comprasData) ? comprasData : []);
          setProdutos(Array.isArray(produtosData) ? produtosData : []);
        }
      } catch {
        if (!cancelled) {
          setCompras([]);
          setProdutos([]);
        }
      } finally {
        if (!cancelled) {
          setLoadingRefs(false);
        }
      }
    }

    loadRefs();
    return () => {
      cancelled = true;
    };
  }, []);

  const compraOptions = useMemo(
    () =>
      compras.map((compra) => ({
        value: String(compra.idDto),
        label: `Compra #${compra.idDto}`
      })),
    [compras]
  );

  const produtoOptions = useMemo(
    () =>
      produtos.map((produto) => ({
        value: String(produto.idDto),
        label: `${produto.codProdutoDto} - ${produto.descProdutoDto}`
      })),
    [produtos]
  );

  const produtoPorId = useMemo(
    () => new Map(produtos.map((produto) => [produto.idDto, produto])),
    [produtos]
  );

  return (
    <CrudPanel
      title="Itens de compra cadastrados"
      resourceLabel="item de compra"
      icon="cart"
      description="Selecione a compra, o produto e informe a quantidade. O ID e gerado automaticamente."
      api={compraItemApi}
      emptyForm={EMPTY_FORM}
      fields={[
        {
          name: "compraIdDto",
          label: "Compra",
          type: "select",
          required: true,
          disabled: loadingRefs || compraOptions.length === 0,
          placeholder: loadingRefs
            ? "Carregando compras..."
            : compraOptions.length === 0
              ? "Nenhuma compra cadastrada"
              : "Selecione uma compra",
          options: compraOptions
        },
        {
          name: "produtoIdDto",
          label: "Produto",
          type: "select",
          required: true,
          disabled: loadingRefs || produtoOptions.length === 0,
          placeholder: loadingRefs
            ? "Carregando produtos..."
            : produtoOptions.length === 0
              ? "Nenhum produto cadastrado"
              : "Selecione um produto",
          options: produtoOptions
        },
        {
          name: "quantidadeDto",
          label: "Quantidade",
          type: "number",
          min: "1",
          step: "1",
          required: true
        }
      ]}
      columns={[
        { key: "compraIdDto", label: "Compra", format: (value) => `Compra #${value}` },
        {
          key: "produtoIdDto",
          label: "Produto",
          format: (value) => {
            const produto = produtoPorId.get(value);
            return produto ? `${produto.codProdutoDto} - ${produto.descProdutoDto}` : value;
          }
        },
        { key: "quantidadeDto", label: "Quantidade" }
      ]}
      mapRow={(item) => ({
        compraIdDto: String(item.compraIdDto ?? ""),
        produtoIdDto: String(item.produtoIdDto ?? ""),
        quantidadeDto: String(item.quantidadeDto ?? "1")
      })}
      buildCreatePayload={(form) => ({
        compraIdDto: Number(form.compraIdDto),
        produtoIdDto: Number(form.produtoIdDto),
        quantidadeDto: Number(form.quantidadeDto)
      })}
      buildUpdatePayload={(form) => ({
        compraIdDto: Number(form.compraIdDto),
        produtoIdDto: Number(form.produtoIdDto),
        quantidadeDto: Number(form.quantidadeDto)
      })}
    />
  );
}
