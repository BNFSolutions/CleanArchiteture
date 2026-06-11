import { useMemo, useState } from "react";
import Icon from "./components/Icon";
import CategoriasPanel from "./components/CategoriasPanel";
import ProdutosPanel from "./components/ProdutosPanel";
import ClientesPanel from "./components/ClientesPanel";
import ComprasPanel from "./components/ComprasPanel";
import CompraItensPanel from "./components/CompraItensPanel";

const MENU = [
  { id: "Dashboard", label: "Dashboard", icon: "dashboard", group: "Geral" },
  { id: "Categorias", label: "Categorias", icon: "categories", group: "Cadastros" },
  { id: "Produtos", label: "Produtos", icon: "products", group: "Cadastros" },
  { id: "Clientes", label: "Clientes", icon: "users", group: "Cadastros" },
  { id: "Compras", label: "Compras", icon: "cart", group: "Vendas" },
  { id: "CompraItens", label: "Compra Itens", icon: "cart", group: "Vendas" },
  { id: "Relatorios", label: "Relatorios", icon: "reports", group: "Analise" },
  { id: "Configuracoes", label: "Configuracoes", icon: "settings", group: "Sistema" }
];

const PAGE_META = {
  Dashboard: { title: "Painel Executivo", subtitle: "Visao geral da operacao em tempo real" },
  Categorias: { title: "Categorias", subtitle: "Gerencie as categorias de produtos" },
  Produtos: { title: "Produtos", subtitle: "Catalogo completo de produtos" },
  Clientes: { title: "Clientes", subtitle: "Cadastro de clientes" },
  Compras: { title: "Compras", subtitle: "Registro de compras dos clientes" },
  CompraItens: { title: "Compra Itens", subtitle: "Produtos vinculados a cada compra" },
  Relatorios: { title: "Relatorios", subtitle: "Indicadores e metricas consolidadas" },
  Configuracoes: { title: "Configuracoes", subtitle: "Preferencias e integracoes do sistema" }
};

const STATS = [
  { label: "Pedidos Hoje", value: "1.284", trend: "+12.8%", up: true, icon: "products", tone: "indigo" },
  { label: "Receita Mensal", value: "R$ 341.900", trend: "+9.2%", up: true, icon: "cart", tone: "green" },
  { label: "Itens em Estoque", value: "8.142", trend: "-1.3%", up: false, icon: "categories", tone: "amber" },
  { label: "SLA Entregas", value: "98.4%", trend: "+0.7%", up: true, icon: "activity", tone: "violet" }
];

const REPORT_MENU = ["Performance", "Estoque", "Financeiro"];

const REPORT_DATA = {
  Performance: [
    { name: "Jan", percent: 61 },
    { name: "Fev", percent: 68 },
    { name: "Mar", percent: 73 },
    { name: "Abr", percent: 77 },
    { name: "Mai", percent: 82 },
    { name: "Jun", percent: 89 }
  ],
  Estoque: [
    { name: "Eletronicos", percent: 72 },
    { name: "Casa", percent: 43 },
    { name: "Moda", percent: 57 },
    { name: "Automotivo", percent: 29 },
    { name: "Esportes", percent: 48 }
  ],
  Financeiro: [
    { name: "Receita", percent: 84 },
    { name: "Custos", percent: 49 },
    { name: "Margem", percent: 66 },
    { name: "Inadimplencia", percent: 22 }
  ]
};

const TIMELINE = [
  { what: "Novo produto cadastrado", who: "Admin", when: "ha 12 min", tone: "green" },
  { what: "Compra #42 finalizada", who: "Sistema", when: "ha 38 min", tone: "indigo" },
  { what: "Estoque abaixo do minimo", who: "Alerta", when: "ha 1h", tone: "amber" },
  { what: "Cliente atualizado", who: "Admin", when: "ha 2h", tone: "violet" }
];

export default function App() {
  const [logged, setLogged] = useState(false);
  const [login, setLogin] = useState({ user: "", pass: "" });
  const [showPassword, setShowPassword] = useState(false);
  const [loginError, setLoginError] = useState("");
  const [activeMenu, setActiveMenu] = useState("Dashboard");
  const [activeReportMenu, setActiveReportMenu] = useState("Performance");
  const [sidebarOpen, setSidebarOpen] = useState(false);

  const menuGroups = useMemo(() => {
    return MENU.reduce((groups, item) => {
      if (!groups[item.group]) {
        groups[item.group] = [];
      }
      groups[item.group].push(item);
      return groups;
    }, {});
  }, []);

  const reportData = useMemo(() => REPORT_DATA[activeReportMenu], [activeReportMenu]);
  const pageMeta = PAGE_META[activeMenu] ?? PAGE_META.Dashboard;

  function handleSignIn(event) {
    event.preventDefault();
    if (login.user === "admin" && login.pass === "123") {
      setLogged(true);
      setLoginError("");
      return;
    }
    setLoginError("Credencial invalida. Use admin e senha 123.");
  }

  function selectMenu(menuId) {
    setActiveMenu(menuId);
    setSidebarOpen(false);
  }

  function renderDashboard() {
    return (
      <div className="stack">
        <div className="stat-grid">
          {STATS.map((stat) => (
            <article key={stat.label} className={`stat-card tone-${stat.tone}`}>
              <div className="stat-icon">
                <Icon name={stat.icon} size={22} />
              </div>
              <div className="stat-body">
                <span className="stat-label">{stat.label}</span>
                <strong className="stat-value">{stat.value}</strong>
                <span className={`stat-trend ${stat.up ? "up" : "down"}`}>
                  <Icon name={stat.up ? "trendUp" : "trendDown"} size={14} />
                  {stat.trend}
                </span>
              </div>
            </article>
          ))}
        </div>

        <div className="grid-2">
          <section className="card">
            <header className="card-head">
              <div>
                <h3>Desempenho mensal</h3>
                <p className="muted">Evolucao consolidada dos ultimos meses</p>
              </div>
              <div className="seg">
                {REPORT_MENU.map((menu) => (
                  <button
                    key={menu}
                    className={menu === activeReportMenu ? "seg-btn active" : "seg-btn"}
                    onClick={() => setActiveReportMenu(menu)}
                  >
                    {menu}
                  </button>
                ))}
              </div>
            </header>
            <div className="bars">
              {reportData.map((item) => (
                <div key={item.name} className="bar-row">
                  <span className="bar-name">{item.name}</span>
                  <div className="bar-track">
                    <div className="bar-fill" style={{ width: `${item.percent}%` }} />
                  </div>
                  <strong className="bar-value">{item.percent}%</strong>
                </div>
              ))}
            </div>
          </section>

          <section className="card">
            <header className="card-head">
              <div>
                <h3>Atividade recente</h3>
                <p className="muted">Ultimos eventos do sistema</p>
              </div>
              <Icon name="activity" size={18} className="muted-icon" />
            </header>
            <ul className="timeline">
              {TIMELINE.map((item, index) => (
                <li key={index} className="timeline-item">
                  <span className={`dot tone-${item.tone}`} />
                  <div>
                    <p className="timeline-what">{item.what}</p>
                    <p className="timeline-meta">
                      <span>{item.who}</span> — <span>{item.when}</span>
                    </p>
                  </div>
                </li>
              ))}
            </ul>
          </section>
        </div>

        <section className="card status-card">
          <header className="card-head">
            <div>
              <h3>Radar operacional</h3>
              <p className="muted">Monitoramento de servicos criticos</p>
            </div>
          </header>
          <div className="status-grid">
            <div className="status-item">
              <span className="muted">Fila de pedidos</span>
              <strong className="pill pill-ok">Normal</strong>
            </div>
            <div className="status-item">
              <span className="muted">APIs externas</span>
              <strong className="pill pill-ok">Estavel</strong>
            </div>
            <div className="status-item">
              <span className="muted">Banco principal</span>
              <strong className="pill pill-info">8 ms</strong>
            </div>
            <div className="status-item">
              <span className="muted">Uso de CPU</span>
              <strong className="pill pill-warn">63%</strong>
            </div>
          </div>
        </section>
      </div>
    );
  }

  function renderSettings() {
    const cards = [
      { icon: "plug", title: "Integracoes", text: "Conectores ativos com ERP e banco de dados." },
      { icon: "shield", title: "Seguranca", text: "Controle de acesso por perfil e trilha de auditoria." },
      { icon: "bell", title: "Notificacoes", text: "Alertas em tempo real para eventos criticos." }
    ];

    return (
      <div className="settings-grid">
        {cards.map((card) => (
          <article key={card.title} className="card setting-card">
            <div className="setting-icon">
              <Icon name={card.icon} size={22} />
            </div>
            <h3>{card.title}</h3>
            <p className="muted">{card.text}</p>
          </article>
        ))}
      </div>
    );
  }

  function renderMainContent() {
    if (activeMenu === "Categorias") return <CategoriasPanel />;
    if (activeMenu === "Produtos") return <ProdutosPanel />;
    if (activeMenu === "Clientes") return <ClientesPanel />;
    if (activeMenu === "Compras") return <ComprasPanel />;
    if (activeMenu === "CompraItens") return <CompraItensPanel />;
    if (activeMenu === "Relatorios") return renderDashboard();
    if (activeMenu === "Configuracoes") return renderSettings();
    return renderDashboard();
  }

  if (!logged) {
    return (
      <main className="auth">
        <div className="auth-brand">
          <div className="auth-brand-inner">
            <div className="brand-logo">
              <Icon name="dashboard" size={26} />
            </div>
            <h1>CleanArchitecture</h1>
            <p>Command Center — Plataforma de gestao integrada para sua operacao.</p>
            <ul className="auth-features">
              <li>
                <Icon name="check" size={16} /> Gestao de produtos e categorias
              </li>
              <li>
                <Icon name="check" size={16} /> Indicadores em tempo real
              </li>
              <li>
                <Icon name="check" size={16} /> Arquitetura limpa e escalavel
              </li>
            </ul>
          </div>
        </div>

        <div className="auth-form-wrap">
          <form className="auth-card" onSubmit={handleSignIn}>
            <span className="eyebrow">Bem-vindo de volta</span>
            <h2>Entrar no sistema</h2>
            <p className="muted">Acesse o painel administrativo</p>

            <label className="field">
              <span>Usuario</span>
              <input
                type="text"
                value={login.user}
                onChange={(event) => setLogin((prev) => ({ ...prev, user: event.target.value }))}
                placeholder="admin"
                autoComplete="username"
              />
            </label>

            <label className="field">
              <span>Senha</span>
              <div className="password-field">
                <input
                  type={showPassword ? "text" : "password"}
                  value={login.pass}
                  onChange={(event) => setLogin((prev) => ({ ...prev, pass: event.target.value }))}
                  placeholder="123"
                  autoComplete="current-password"
                />
                <button
                  type="button"
                  className="icon-btn ghost"
                  onClick={() => setShowPassword((prev) => !prev)}
                  aria-label={showPassword ? "Ocultar senha" : "Mostrar senha"}
                >
                  <Icon name={showPassword ? "eyeOff" : "eye"} size={18} />
                </button>
              </div>
            </label>

            {loginError && (
              <p className="alert alert-error">
                <Icon name="alert" size={16} /> {loginError}
              </p>
            )}

            <button type="submit" className="btn btn-primary btn-block">
              Entrar
            </button>
            <p className="auth-hint muted">
              Dica: use <strong>admin</strong> / <strong>123</strong>
            </p>
          </form>
        </div>
      </main>
    );
  }

  return (
    <div className={`shell ${sidebarOpen ? "sidebar-open" : ""}`}>
      <div className="scrim" onClick={() => setSidebarOpen(false)} />

      <aside className="sidebar">
        <div className="sidebar-brand">
          <div className="brand-logo">
            <Icon name="dashboard" size={20} />
          </div>
          <div className="brand-text">
            <strong>CleanArch</strong>
            <small>Control Center</small>
          </div>
        </div>

        <nav className="sidebar-nav">
          {Object.entries(menuGroups).map(([group, items]) => (
            <div key={group} className="nav-group">
              <span className="nav-group-label">{group}</span>
              {items.map((item) => (
                <button
                  key={item.id}
                  className={activeMenu === item.id ? "nav-item active" : "nav-item"}
                  onClick={() => selectMenu(item.id)}
                >
                  <Icon name={item.icon} size={19} />
                  <span>{item.label}</span>
                </button>
              ))}
            </div>
          ))}
        </nav>

        <div className="sidebar-foot">
          <div className="sidebar-user">
            <div className="avatar">AD</div>
            <div className="sidebar-user-info">
              <strong>Administrador</strong>
              <small>admin</small>
            </div>
          </div>
          <button
            type="button"
            className="icon-btn"
            onClick={() => setLogged(false)}
            aria-label="Sair"
            title="Sair"
          >
            <Icon name="logout" size={18} />
          </button>
        </div>
      </aside>

      <div className="main">
        <header className="topbar">
          <button
            type="button"
            className="icon-btn only-mobile"
            onClick={() => setSidebarOpen(true)}
            aria-label="Abrir menu"
          >
            <Icon name="menu" size={20} />
          </button>
          <div className="topbar-title">
            <h1>{pageMeta.title}</h1>
            <p className="muted">{pageMeta.subtitle}</p>
          </div>
          <div className="topbar-actions">
            <div className="search">
              <Icon name="search" size={16} />
              <input type="text" placeholder="Buscar..." />
            </div>
          </div>
        </header>

        <section className="content">{renderMainContent()}</section>
      </div>
    </div>
  );
}
