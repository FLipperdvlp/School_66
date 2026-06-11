import { useState, useEffect, useRef } from "react";
import "./App.css";

// ─── Types ────────────────────────────────────────────────────────────────────
interface User {
  email: string;
  fullName: string;
  role: "Admin" | "User";
}

interface StudentForm {
  id: number;
  title: string;
  type: string;
  createdAt: string;
  status: "Новий" | "Обробляється" | "Виконано" | string;
}

type Page =
  | "home"
  | "form"
  | "student"
  | "parent"
  | "login"
  | "register"
  | "admin"
  | "myForms";

// ─── Mock data ────────────────────────────────────────────────────────────────
const MOCK_FORMS: StudentForm[] = [
  { id: 1, title: "Запит щодо розкладу занять", type: "Учень", createdAt: "2025-03-15", status: "Виконано" },
  { id: 2, title: "Питання про оцінювання", type: "Батьки", createdAt: "2025-03-20", status: "Обробляється" },
  { id: 3, title: "Прохання про довідку", type: "Учень", createdAt: "2025-04-01", status: "Новий" },
];

// ─── Layout ───────────────────────────────────────────────────────────────────
function Layout({
  user, onNavigate, onLogout, children, toast,
}: {
  user: User | null;
  onNavigate: (p: Page) => void;
  onLogout: () => void;
  children: React.ReactNode;
  toast: string | null;
}) {
  const [scrolled, setScrolled] = useState(false);
  const [scrollPct, setScrollPct] = useState(0);

  useEffect(() => {
    const onScroll = () => {
      setScrolled(window.scrollY > 60);
      const pct =
        (window.scrollY / (document.documentElement.scrollHeight - window.innerHeight)) * 100;
      setScrollPct(pct);
    };
    window.addEventListener("scroll", onScroll);
    return () => window.removeEventListener("scroll", onScroll);
  }, []);

  return (
    <>
      <div className="scroll-indicator" style={{ width: `${scrollPct}%` }} />

      <header id="header" className={scrolled ? "scrolled" : ""}>
        <div className="container">
          <div className="header-content">
            <span className="logo" onClick={() => onNavigate("home")}>
              Школа<span>№66</span>
            </span>
            <nav>
              <ul>
                <li><a href="#home" onClick={() => onNavigate("home")}>Головна</a></li>
                <li><a href="#about">Про нас</a></li>
                <li><a href="#programs">Програми</a></li>
                <li><a href="#news">Новини</a></li>
                <li><a href="#contact">Контакти</a></li>
                <li>
                  <button className="nav-link-btn" onClick={() => onNavigate("form")}>
                    Подати запит
                  </button>
                </li>
                {user ? (
                  <>
                    {user.role === "Admin" && (
                      <li>
                        <button className="nav-link-btn" onClick={() => onNavigate("admin")}>
                          Адмін
                        </button>
                      </li>
                    )}
                    <li>
                      <button className="nav-link-btn" onClick={onLogout}>
                        Вийти
                      </button>
                    </li>
                  </>
                ) : (
                  <>
                    <li>
                      <button className="nav-link-btn" onClick={() => onNavigate("login")}>
                        Вхід
                      </button>
                    </li>
                    <li>
                      <button className="nav-link-btn" onClick={() => onNavigate("register")}>
                        Реєстрація
                      </button>
                    </li>
                  </>
                )}
              </ul>
            </nav>
          </div>
        </div>
      </header>

      {toast && <div className="success-toast">{toast}</div>}

      <main>{children}</main>

      <footer>
        <div className="container">
          <p>© 2025 Школа №66, Дніпро — Developed by HLIB RENKAS</p>
        </div>
      </footer>
    </>
  );
}

// ─── Home ─────────────────────────────────────────────────────────────────────
function HomePage({ onNavigate }: { onNavigate: (p: Page) => void }) {
  const statRef = useRef<HTMLDivElement>(null);
  const [counts, setCounts] = useState([0, 0, 0]);
  const targets = [850, 45, 52];
  const animated = useRef(false);

  useEffect(() => {
    const observer = new IntersectionObserver(
      (entries) => {
        if (entries[0].isIntersecting && !animated.current) {
          animated.current = true;
          targets.forEach((target, i) => {
            let current = 0;
            const step = target / 80;
            const timer = setInterval(() => {
              current = Math.min(current + step, target);
              setCounts((prev) => {
                const next = [...prev];
                next[i] = Math.floor(current);
                return next;
              });
              if (current >= target) clearInterval(timer);
            }, 16);
          });
        }
      },
      { threshold: 0.4 }
    );
    if (statRef.current) observer.observe(statRef.current);
    return () => observer.disconnect();
  }, []);

  const [contactForm, setContactForm] = useState({ name: "", email: "", subject: "", message: "" });
  const [submitted, setSubmitted] = useState(false);

  const handleContact = (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitted(true);
    setContactForm({ name: "", email: "", subject: "", message: "" });
    setTimeout(() => setSubmitted(false), 4000);
  };

  const programs = [
    { icon: "📚", title: "Початкова школа", desc: "Програма для учнів 1–4 класів з акцентом на основні навички." },
    { icon: "🔬", title: "STEM освіта", desc: "Наука, технології, інженерія та математика." },
    { icon: "🎨", title: "Мистецькі класи", desc: "Музика, образотворче мистецтво, театр." },
    { icon: "🌍", title: "Мовні програми", desc: "Іноземні мови з носіями та міжнародними сертифікаціями." },
    { icon: "💻", title: "IT класи", desc: "Програмування, робототехніка, цифрові технології." },
    { icon: "🏃", title: "Спортивні секції", desc: "Різноманітні спортивні програми для фізичного розвитку." },
  ];

  const news = [
    { icon: "🏆", date: "15 березня 2025", title: "Перемога у міському конкурсі", body: "Учні здобули перше місце з математики та фізики." },
    { icon: "🔬", date: "10 березня 2025", title: "Нова лабораторія", body: "Відкрито сучасну лабораторію для дослідів з хімії та біології." },
    { icon: "🌟", date: "5 березня 2025", title: "День відкритих дверей", body: "Запрошуємо майбутніх першокласників та батьків 20 березня." },
  ];

  return (
    <>
      {/* Hero */}
      <section id="home" className="hero">
        <div className="container">
          <div className="hero-content">
            <span className="hero-eyebrow">Дніпро · Заснована 1972</span>
            <h1>Освіта, що <em>готує</em> до майбутнього</h1>
            <p>
              Школа №66 — сучасний навчальний заклад, де кожен учень отримує
              якісні знання та розвивається як особистість.
            </p>
            <a href="#about" className="btn-hero">Дізнатися більше</a>
          </div>
        </div>
      </section>

      {/* About */}
      <section id="about" className="about">
        <div className="container">
          <div className="section-title">
            <div className="section-label">Про нас</div>
            <h2>Понад 50 років якісної освіти</h2>
          </div>
          <div className="about-content">
            <div className="about-text">
              <p>
                Школа №66 у Дніпрі — сучасний навчальний заклад, що надає якісну
                освіту вже понад 50 років. Ми об'єднуємо кращі педагогічні традиції
                з інноваційними методами навчання.
              </p>
              <p>
                Наша місія — виховати всебічно розвинену особистість, готову до
                викликів сучасного світу. Кожен учень отримує індивідуальну увагу
                та підтримку в досягненні своїх цілей.
              </p>
            </div>
            <div className="stats" ref={statRef}>
              {[
                { num: counts[0], label: "Учнів навчається зараз" },
                { num: counts[1], label: "Досвідчених вчителів" },
                { num: counts[2], label: "Роки роботи" },
              ].map((s) => (
                <div key={s.label} className="stat-item">
                  <div className="stat-number">{s.num}</div>
                  <p>{s.label}</p>
                </div>
              ))}
            </div>
          </div>
        </div>
      </section>

      {/* Programs */}
      <section id="programs" className="programs">
        <div className="container">
          <div className="section-title">
            <div className="section-label">Напрямки</div>
            <h2>Наші програми</h2>
          </div>
          <div className="programs-grid">
            {programs.map((p) => (
              <div key={p.title} className="program-card">
                <span className="program-icon">{p.icon}</span>
                <h3>{p.title}</h3>
                <p>{p.desc}</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* News */}
      <section id="news" className="news">
        <div className="container">
          <div className="section-title">
            <div className="section-label">Останні події</div>
            <h2>Новини школи</h2>
          </div>
          <div className="news-grid">
            {news.map((n) => (
              <div key={n.title} className="news-card">
                <div className="news-image">{n.icon}</div>
                <div className="news-content">
                  <div className="news-date">{n.date}</div>
                  <h3>{n.title}</h3>
                  <p>{n.body}</p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Contact */}
      <section id="contact" className="contact">
        <div className="container">
          <div className="section-title">
            <div className="section-label">Зв'язок</div>
            <h2>Контакти</h2>
          </div>
          <div className="contact-content">
            <div className="contact-info">
              {[
                { icon: "📍", label: "Адреса", val: "вул. Освітня, 66, Дніпро, 49000" },
                { icon: "📞", label: "Телефон", val: "+38 (056) 123-45-67" },
                { icon: "✉️", label: "Email", val: "info@school66.dp.ua" },
                { icon: "🕒", label: "Робочі години", val: "Пн–Пт: 8:00 – 17:00" },
              ].map((c) => (
                <div key={c.label} className="contact-item">
                  <i>{c.icon}</i>
                  <div>
                    <strong>{c.label}:</strong><br />{c.val}
                  </div>
                </div>
              ))}
            </div>
            <form className="contact-form" onSubmit={handleContact}>
              {submitted && (
                <div className="error-message success">
                  Дякуємо! Ми зв'яжемося з вами найближчим часом.
                </div>
              )}
              {(["name", "email", "subject"] as const).map((field) => (
                <div key={field} className="form-group">
                  <label htmlFor={field}>
                    {field === "name" ? "Ім'я" : field === "email" ? "Email" : "Тема"}
                  </label>
              
                  <input
                    id={field}
                    type={field === "email" ? "email" : "text"}
                    value={contactForm[field]}
                    onChange={(e) =>
                      setContactForm({ ...contactForm, [field]: e.target.value })
                    }
                    required
                  />
                </div>
              ))}
              <div className="form-group">
                <label htmlFor="message">Повідомлення</label>
                <textarea
                  id="message"
                  rows={4}
                  value={contactForm.message}
                  onChange={(e) =>
                    setContactForm({ ...contactForm, message: e.target.value })
                  }
                  required
                />
              </div>
              <button type="submit" className="btn-submit-contact">Надіслати</button>
            </form>
          </div>
        </div>
      </section>
    </>
  );
}

// ─── Form index ───────────────────────────────────────────────────────────────
function FormPage({ user, onNavigate }: { user: User | null; onNavigate: (p: Page) => void }) {
  const handleMyRequests = () => {
    if (!user) {
      alert("Щоб переглянути 'Мої запити', потрібно увійти або зареєструватися.");
      return;
    }
    onNavigate("myForms");
  };

  return (
    <div className="container form-index">
      <div className="form-nav">
        <div>
          <div className="section-label">Навчальна частина</div>
          <h2>Форма подання запиту</h2>
        </div>
        <button className="requests-btn" onClick={handleMyRequests}>
          Мої запити →
        </button>
      </div>
      <div className="form-choices">
        <p>Виберіть, від кого робите запит:</p>
        <div className="form-choices-row">
          <button className="modern-btn" onClick={() => onNavigate("student")}>
            Учень
          </button>
          <button className="modern-btn secondary" onClick={() => onNavigate("parent")}>
            Батьки
          </button>
        </div>
      </div>
    </div>
  );
}

// ─── Contact method selector (shared) ────────────────────────────────────────
function ContactSelector({
  value, onChange, variant,
}: {
  value: string;
  onChange: (v: string) => void;
  variant: "student" | "parent";
}) {
  const options = [
    { method: "Telegram", icon: "telegram", label: "Telegram", emoji: "✈️" },
    { method: "Email", icon: "email", label: "Email", emoji: "📧" },
    { method: "Phone", icon: "phone", label: "Телефон", emoji: "📞" },
  ];
  return (
    <div className="contact-grid">
      {options.map((c) => (
        <button
          type="button"
          key={c.method}
          className={`contact-card ${value === c.method ? "active" : ""}`}
          onClick={() => onChange(c.method)}
        >
          <div className={`contact-icon ${c.icon}`}>{c.emoji}</div>
          <span className="contact-label">{c.label}</span>
          <span className="check-mark">✓</span>
        </button>
      ))}
    </div>
  );
}

// ─── Student form ─────────────────────────────────────────────────────────────
function StudentPage({ onSuccess }: { onSuccess: () => void }) {
  const [form, setForm] = useState({
    firstName: "", lastName: "", className: "", contactMethod: "", requestText: "",
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!form.contactMethod) { alert("Оберіть спосіб зв'язку"); return; }
    onSuccess();
  };

  return (
    <div className="form-page-bg">
      <div className="container">
        <div className="form-wrapper">
          <div className="form-header">
            <div className="form-icon">💬</div>
            <h1>Запит для учнів</h1>
            <p>Заповніть форму і ми зв'яжемося з вами найближчим часом</p>
          </div>

          <form onSubmit={handleSubmit}>
            <div className="form-row-2" style={{ marginBottom: 18 }}>
              {(["firstName", "lastName"] as const).map((f) => (
                <div key={f}>
                  <label className="field-label">
                    {f === "firstName" ? "Ім'я" : "Прізвище"}
                  </label>
                  <input
                    className="form-control-modern"
                    placeholder={f === "firstName" ? "Ім'я" : "Прізвище"}
                    value={form[f]}
                    onChange={(e) => setForm({ ...form, [f]: e.target.value })}
                    required
                  />
                </div>
              ))}
            </div>

            <div className="form-field">
              <label className="field-label">Клас</label>
              <input
                className="form-control-modern"
                placeholder="Наприклад: 10-А"
                value={form.className}
                onChange={(e) => setForm({ ...form, className: e.target.value })}
                required
              />
              <span className="field-hint">Наприклад: 10-А</span>
            </div>

            <div className="form-field">
              <label className="field-label">Спосіб зв'язку</label>
              <ContactSelector
                value={form.contactMethod}
                onChange={(v) => setForm({ ...form, contactMethod: v })}
                variant="student"
              />
            </div>

            <div className="form-field">
              <label className="field-label">Ваш запит</label>
              <textarea
                className="form-control-modern"
                placeholder="Опишіть ваш запит..."
                rows={5}
                value={form.requestText}
                onChange={(e) => setForm({ ...form, requestText: e.target.value })}
              />
            </div>

            <button type="submit" className="btn-submit-form">
              Відправити запит
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}

// ─── Parent form ──────────────────────────────────────────────────────────────
function ParentPage({ onSuccess }: { onSuccess: () => void }) {
  const [form, setForm] = useState({
    parentFirstName: "", parentLastName: "", childFullName: "",
    childClass: "", contactMethod: "", requestText: "",
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!form.contactMethod) { alert("Оберіть спосіб зв'язку"); return; }
    onSuccess();
  };

  return (
    <div className="form-page-bg">
      <div className="container">
        <div className="form-wrapper">
          <div className="form-header">
            <div className="form-icon parent">👥</div>
            <h1>Запит для батьків</h1>
            <p>Заповніть форму і ми обов'язково зв'яжемося з вами</p>
          </div>

          <form onSubmit={handleSubmit}>
            <div className="form-row-2" style={{ marginBottom: 18 }}>
              {(["parentFirstName", "parentLastName"] as const).map((f) => (
                <div key={f}>
                  <label className="field-label">
                    {f === "parentFirstName" ? "Ваше ім'я" : "Ваше прізвище"}
                  </label>
                  <input
                    className="form-control-modern"
                    placeholder={f === "parentFirstName" ? "Ім'я" : "Прізвище"}
                    value={form[f]}
                    onChange={(e) => setForm({ ...form, [f]: e.target.value })}
                    required
                  />
                </div>
              ))}
            </div>

            <div className="form-field">
              <label className="field-label">ПІБ дитини</label>
              <input
                className="form-control-modern"
                placeholder="Іваненко Марія Олегівна"
                value={form.childFullName}
                onChange={(e) => setForm({ ...form, childFullName: e.target.value })}
                required
              />
              <span className="field-hint">Прізвище, ім'я та по батькові</span>
            </div>

            <div className="form-field">
              <label className="field-label">Клас дитини</label>
              <input
                className="form-control-modern"
                placeholder="Наприклад: 7-Б"
                value={form.childClass}
                onChange={(e) => setForm({ ...form, childClass: e.target.value })}
                required
              />
            </div>

            <div className="form-field">
              <label className="field-label">Спосіб зв'язку</label>
              <ContactSelector
                value={form.contactMethod}
                onChange={(v) => setForm({ ...form, contactMethod: v })}
                variant="parent"
              />
            </div>

            <div className="form-field">
              <label className="field-label">Ваш запит</label>
              <textarea
                className="form-control-modern"
                placeholder="Опишіть ваш запит..."
                rows={5}
                value={form.requestText}
                onChange={(e) => setForm({ ...form, requestText: e.target.value })}
              />
            </div>

            <button type="submit" className="btn-submit-form parent">
              Відправити запит
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}

// ─── Login ────────────────────────────────────────────────────────────────────
function LoginPage({ onLogin, onNavigate }: { onLogin: (u: User) => void; onNavigate: (p: Page) => void }) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (email === "admin@school66.ua" && password === "admin") {
      onLogin({ email, fullName: "Адміністратор", role: "Admin" });
    } else if (email && password.length >= 4) {
      onLogin({ email, fullName: email.split("@")[0], role: "User" });
    } else {
      setError("Невірний email або пароль");
      setTimeout(() => setError(""), 3000);
    }
  };

  return (
    <div className="auth-form-container">
      <h2>Вхід</h2>
      <p className="auth-subtitle">Увійдіть до свого акаунту</p>
      {error && <div className="error-message error">{error}</div>}
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="email">Email</label>
          <input type="email" id="email" value={email} onChange={(e) => setEmail(e.target.value)} required placeholder="you@example.com" />
        </div>
        <div className="form-group">
          <label htmlFor="password">Пароль</label>
          <input type="password" id="password" value={password} onChange={(e) => setPassword(e.target.value)} required placeholder="••••••••" />
        </div>
        <button type="submit" className="auth-btn" style={{ marginTop: 6 }}>Увійти</button>
      </form>
      <div className="auth-divider">або</div>
      <button type="button" className="auth-btn google">🔍 Увійти через Google</button>
      <p>
        Немає акаунту?{" "}
        <a href="#" onClick={(e) => { e.preventDefault(); onNavigate("register"); }}>
          Зареєструватися
        </a>
      </p>
    </div>
  );
}

// ─── Register ─────────────────────────────────────────────────────────────────
function RegisterPage({ onRegister, onNavigate }: { onRegister: (u: User) => void; onNavigate: (p: Page) => void }) {
  const [form, setForm] = useState({ fullName: "", email: "", password: "", confirmPassword: "" });
  const [error, setError] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (form.password !== form.confirmPassword) { setError("Паролі не збігаються"); return; }
    if (form.password.length < 4) { setError("Пароль має бути щонайменше 4 символи"); return; }
    onRegister({ email: form.email, fullName: form.fullName, role: "User" });
  };

  return (
    <div className="auth-form-container">
      <h2>Реєстрація</h2>
      <p className="auth-subtitle">Створіть новий акаунт</p>
      {error && <div className="error-message error">{error}</div>}
      <form onSubmit={handleSubmit}>
        {(["fullName", "email", "password", "confirmPassword"] as const).map((f) => (
          <div key={f} className="form-group">
            <label>
              {f === "fullName" ? "Повне ім'я" : f === "email" ? "Email" : f === "password" ? "Пароль" : "Підтвердіть пароль"}
            </label>
            <input
              type={f.includes("assword") ? "password" : f === "email" ? "email" : "text"}
              value={form[f]}
              onChange={(e) => setForm({ ...form, [f]: e.target.value })}
              required
              placeholder={
                f === "fullName" ? "Іваненко Іван" :
                f === "email" ? "you@example.com" :
                f === "password" ? "••••••••" : "••••••••"
              }
            />
          </div>
        ))}
        <button type="submit" className="auth-btn green" style={{ marginTop: 6 }}>
          Зареєструватися
        </button>
      </form>
      <p>
        Вже є акаунт?{" "}
        <a href="#" onClick={(e) => { e.preventDefault(); onNavigate("login"); }}>
          Увійти
        </a>
      </p>
    </div>
  );
}

// ─── Admin ────────────────────────────────────────────────────────────────────
function AdminPage() {
  return (
    <div className="admin-panel">
      <div className="section-label">Система управління</div>
      <h2>Адмін Панель</h2>
      <p>Ласкаво просимо, адміністраторе!</p>
      <div className="admin-actions">
        <button className="admin-btn">📋 Форми та запити</button>
        <button className="admin-btn">👤 Користувачі</button>
        <button className="admin-btn">📰 Новини</button>
      </div>
    </div>
  );
}

// ─── My Forms ─────────────────────────────────────────────────────────────────
function MyFormsPage({ forms, onNavigate }: { forms: StudentForm[]; onNavigate: (p: Page) => void }) {
  const statusClass: Record<string, string> = {
    "Новий": "status-new",
    "Обробляється": "status-processing",
    "Виконано": "status-completed",
  };

  return (
    <div className="container get-forms-page">
      <div className="page-header">
        <div className="section-label">Кабінет</div>
        <h1>Мої запити</h1>
        <p>Перегляд та відстеження статусу ваших звернень</p>
      </div>

      {forms.length === 0 ? (
        <div className="empty-state">
          <span className="empty-icon">📭</span>
          <h3>Запитів поки немає</h3>
          <p>Ви ще не подали жодного запиту. Створіть перший — він з'явиться тут.</p>
          <button className="modern-btn" onClick={() => onNavigate("form")}>
            Подати запит
          </button>
        </div>
      ) : (
        <>
          <div className="table-card">
            <div style={{ overflowX: "auto" }}>
              <table className="requests-table">
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>Заголовок</th>
                    <th>Тип</th>
                    <th>Дата</th>
                    <th>Статус</th>
                  </tr>
                </thead>
                <tbody>
                  {forms.map((req) => (
                    <tr key={req.id} className="request-row">
                      <td><span className="id-badge">#{req.id}</span></td>
                      <td><span style={{ fontWeight: 600 }}>{req.title}</span></td>
                      <td><span style={{ color: "var(--muted)" }}>{req.type}</span></td>
                      <td>
                        <span style={{ color: "var(--muted)" }}>
                          {new Date(req.createdAt).toLocaleDateString("uk-UA")}
                        </span>
                      </td>
                      <td>
                        <span className={`status-badge ${statusClass[req.status] ?? "status-default"}`}>
                          {req.status}
                        </span>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
          <p style={{ color: "var(--muted)", fontSize: "0.83rem", marginTop: 12 }}>
            Всього запитів: {forms.length}
          </p>
        </>
      )}
    </div>
  );
}

// ─── App ──────────────────────────────────────────────────────────────────────
export default function App() {
  const [page, setPage] = useState<Page>("home");
  const [user, setUser] = useState<User | null>(null);
  const [toast, setToast] = useState<string | null>(null);
  const [forms, setForms] = useState<StudentForm[]>(MOCK_FORMS);

  const showToast = (msg: string) => {
    setToast(msg);
    setTimeout(() => setToast(null), 3500);
  };

  const handleLogin = (u: User) => {
    setUser(u);
    showToast(`Вітаємо, ${u.fullName}!`);
    setPage("home");
  };

  const handleRegister = (u: User) => {
    setUser(u);
    showToast("Реєстрацію успішно завершено!");
    setPage("home");
  };

  const handleLogout = () => {
    setUser(null);
    showToast("Ви вийшли з системи.");
    setPage("home");
  };

  const handleFormSuccess = (type: "Учень" | "Батьки") => {
    const newForm: StudentForm = {
      id: forms.length + 1,
      title: `Запит від ${type === "Учень" ? "учня" : "батьків"} #${forms.length + 1}`,
      type,
      createdAt: new Date().toISOString().split("T")[0],
      status: "Новий",
    };
    setForms([...forms, newForm]);
    showToast("Запит успішно надіслано!");
    setPage("form");
  };

  const renderPage = () => {
    switch (page) {
      case "home":    return <HomePage onNavigate={setPage} />;
      case "form":    return <FormPage user={user} onNavigate={setPage} />;
      case "student": return <StudentPage onSuccess={() => handleFormSuccess("Учень")} />;
      case "parent":  return <ParentPage onSuccess={() => handleFormSuccess("Батьки")} />;
      case "login":   return <LoginPage onLogin={handleLogin} onNavigate={setPage} />;
      case "register":return <RegisterPage onRegister={handleRegister} onNavigate={setPage} />;
      case "admin":   return user?.role === "Admin" ? <AdminPage /> : <LoginPage onLogin={handleLogin} onNavigate={setPage} />;
      case "myForms": return <MyFormsPage forms={forms} onNavigate={setPage} />;
      default:        return <HomePage onNavigate={setPage} />;
    }
  };

  return <Layout user={user} onNavigate={setPage} onLogout={handleLogout} toast={toast}>{renderPage()}</Layout>;
}