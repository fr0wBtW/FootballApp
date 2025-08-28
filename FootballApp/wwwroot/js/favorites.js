document.addEventListener("DOMContentLoaded", async function () {
    const response = await fetch('/api/favorites');
    const favoriteIds = await response.json();

    document.querySelectorAll("table").forEach(table => {
        const tbody = table.querySelector("tbody");

        table.querySelectorAll(".favorite-btn").forEach(function (btn) {
            const row = btn.closest("tr");
            const matchId = parseInt(row.dataset.matchId);

            if (favoriteIds.includes(matchId)) {
                const icon = btn.querySelector("i");
                icon.classList.remove("fa-regular");
                icon.classList.add("fa-solid", "active");
                row.classList.add("favorite-row");
            }

            btn.addEventListener("click", async function () {
                const icon = btn.querySelector("i");

                if (icon.classList.contains("fa-regular")) {
                    icon.classList.remove("fa-regular");
                    icon.classList.add("fa-solid", "active");
                    row.classList.add("favorite-row");

                    await fetch(`/api/favorites/add?matchId=${matchId}`, { method: 'POST' });

                } else {
                    icon.classList.remove("fa-solid", "active");
                    icon.classList.add("fa-regular");
                    row.classList.remove("favorite-row");

                    await fetch(`/api/favorites/remove?matchId=${matchId}`, { method: 'POST' });
                }

                reorderTable(tbody);
            });
        });

        function reorderTable(tbody) {
            const rows = Array.from(tbody.querySelectorAll("tr"));
            rows.sort((a, b) => {
                const aFav = a.classList.contains("favorite-row") ? 0 : 1;
                const bFav = b.classList.contains("favorite-row") ? 0 : 1;
                return aFav - bFav;
            });
            rows.forEach(r => tbody.appendChild(r));
        }
        reorderTable(tbody); 
    });
});
