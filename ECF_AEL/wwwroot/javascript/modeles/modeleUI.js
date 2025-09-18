import { listerModeles, ajouterModele } from "./modeleService.js";

export async function chargerModeles() {
  const modeles = await listerModeles();
  const select = document.getElementById("modele");
  select.innerHTML = '<option value="">-- Choisir un modèle --</option>';

  modeles.forEach(m => {
    const opt = document.createElement("option");
    opt.value = m.nomModeleVehicule; 
    opt.textContent = `${m.nomModeleVehicule} (${m.marque}, ${m.annee})`;
    select.appendChild(opt);
  });
}

document.getElementById("modeleForm").addEventListener("submit", async (e) => {
  e.preventDefault();

  const modele = {
    nomModeleVehicule: document.getElementById("nomModele").value,
    marque: document.getElementById("marque").value,
    annee: document.getElementById("annee").value,
    dateAchat: document.getElementById("dateAchat").value
  };

  try {
    await ajouterModele(modele);
    await chargerModeles(); // recharge le dropdown avec le nouveau modèle

    // Fermer la modale
    const modalEl = document.getElementById("ajoutModeleModal");
    const modal = bootstrap.Modal.getInstance(modalEl);
    modal.hide();

    // Enlever le focus du bouton pour éviter le warning aria-hidden
    document.activeElement.blur();

    // Réinitialiser le formulaire
    e.target.reset();

  } catch (err) {
    console.error("Erreur ajout modèle :", err);
    alert("Impossible d’ajouter le modèle");
  }
});

// Charger modèles dès le départ
chargerModeles();
