let currentCard = null;

document.addEventListener('DOMContentLoaded', () => {
    const cardPlaceholder = document.querySelector('.card-placeholder');
    const btnTengui = document.querySelector('.btn-right');
    const btnNoTengui = document.querySelector('.btn-left');

    async function loadCard() {
        try {
            const response = await fetch('/Card/GetRandomCard');
            if (!response.ok) throw new Error('Error al cargar carta');
            const card = await response.json();
            currentCard = card;

            cardPlaceholder.innerHTML = `
                <img src="${card.images.large}" alt="${card.name}" style="max-width:100%; max-height:100%;">
            `;
        } catch (err) {
            console.error(err);
            cardPlaceholder.textContent = "No se pudo cargar la carta.";
        }
    }

    async function saveCard(cardId) {
        try {
            await fetch('/Card/Save', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ cardId })
            });
        } catch (err) {
            console.error('Error al guardar carta', err);
        }
    }

    btnTengui.addEventListener('click', async () => {
        if (currentCard) {
            await saveCard(currentCard.id);
            await loadCard();
        }
    });

    btnNoTengui.addEventListener('click', loadCard);

    loadCard(); // cargar la primera carta
});

document.addEventListener("DOMContentLoaded", () => {
    const path = window.location.pathname;

    if (path.endsWith("index.html")) {
        loadCard(); // juego principal
    }

    if (path.endsWith("miscartas.html")) {
        loadMyCards(); // mis cartas
    }
});

async function loadCard() {
    const cardPlaceholder = document.querySelector(".card-placeholder");
    if (!cardPlaceholder) return;

    try {
        const response = await fetch("/Card/GetRandomCard");
        const card = await response.json();
        cardPlaceholder.innerHTML = `<img src="${card.images.large}" alt="${card.name}" style="max-width:100%; max-height:100%;">`;
        window.currentCard = card;
    } catch {
        cardPlaceholder.textContent = "Error al cargar carta.";
    }
}

async function saveCard(cardId) {
    await fetch("/Card/Save", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ cardId })
    });
}

async function loadMyCards() {
    const container = document.getElementById("mis-cartas");
    if (!container) return;

    try {
        const res = await fetch("/Card/MyCards");
        const cards = await res.json();

        if (cards.length === 0) {
            container.innerHTML = "<p style='color:white'>No tienes cartas guardadas aún.</p>";
            return;
        }

        for (const card of cards) {
            const res = await fetch(`/Card/GetCard/${card.cardId}`);
            const data = await res.json();

            const img = document.createElement("img");
            img.src = data.images.small;
            img.alt = data.name;
            img.style.width = "120px";
            img.style.margin = "10px";
            img.style.borderRadius = "8px";
            container.appendChild(img);
        }
    } catch (err) {
        container.innerHTML = "<p style='color:white'>Error al cargar tus cartas.</p>";
        console.error(err);
    }
}


