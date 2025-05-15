# Tengui No Tengui

Juego web tipo “Tinder” donde puedes marcar si tienes o no una carta Pokémon. Las cartas marcadas como “Tengui” se almacenan y pueden consultarse más adelante en una galería personalizada.

---

## Tecnologías utilizadas

- **Frontend**: HTML, CSS, JavaScript
- **Backend**: ASP.NET Core MVC (C#)
- **Base de datos**: SQLite
- **API**: [Pokémon TCG API](https://docs.pokemontcg.io/)
- **Arquitectura**: Modelo Vista Controlador (MVC)

---

## Funcionamiento del juego

1. El usuario navega por pantallas de introducción hasta llegar al login.
2. Desde el login, accede al juego principal (`index.html`).
3. Se muestra una carta aleatoria obtenida desde la API de Pokémon TCG.
4. El usuario pulsa:
   - **Tengui** → la carta se guarda en la base de datos.
   - **No Tengui** → se pasa a otra carta sin guardarla.
5. En la sección **Mis Cartas**, se muestra una galería con todas las cartas que el usuario ha marcado como “Tengui”.

---

## Estructura del proyecto

TenguiNoTengui/
├── wwwroot/
│ ├── css/
│ ├── js/
│ ├── media/
│ ├── index.html
│ ├── miscartas.html
│ ├── primera.html
│ ├── segunda.html
│ └── tercera.html
├── Controllers/
│ └── CardController.cs
├── Data/
│ └── AppDbContext.cs 
├── Models/
│ ├── Card.cs
│ ├── PokemonApiResponse.cs
│ └── CardDto.cs
├── Migrations/
├── appsettings.json
├── Program.cs 
└── README.md

---

## Enlace Web

michaelpardo-001-site1.jtempurl.com/primera.html

---


## Mejoras futuras

- Autenticación real de usuarios
- Filtrar de cartas por tipo, rareza o expansión
- Sonidos y animaciones
- Poder de eliminar cartas de la colección

---

## Autores

- Nombre: Hugo Martín Pardo & Michael Gabriel Pardo Cadenas
- 2DAW
- PF MP03B/MP09
