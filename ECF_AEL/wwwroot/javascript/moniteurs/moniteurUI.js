import { ajouterMoniteur, listerMoniteurs, supprimerMoniteur, modifierActivite } from "./moniteurService.js";

// ajouter moniteur
document.getElementById("moniteurForm").addEventListener("submit", async (e) => {
  e.preventDefault();

  const formValues = {
    nom: document.getElementById("nomMoniteur").value,
    prenom: document.getElementById("prenomMoniteur").value,
    dateNaissance: document.getElementById("dateNaissance").value,
    dateEmbauche: document.getElementById("dateEmbauche").value,
    activite: document.querySelector('input[name="activite"]:checked')?.value == "true"
  };

  try {
    await ajouterMoniteur(formValues);
    await chargerMoniteurs(); 
    e.target.reset();

    // Toast succès
    document.getElementById("moniteurToast").classList.remove("bg-danger");
    document.getElementById("moniteurToast").classList.add("bg-success");
    document.getElementById("moniteurToastMessage").textContent = " Moniteur ajouté avec succès !";

    const toast = new bootstrap.Toast(document.getElementById("moniteurToast"));
    toast.show();

  } catch (err) {
    console.error("Erreur :", err);

    //  Toast erreur
    document.getElementById("moniteurToast").classList.remove("bg-success");
    document.getElementById("moniteurToast").classList.add("bg-danger");
    document.getElementById("moniteurToastMessage").textContent = " Erreur : " + err.message;

    const toast = new bootstrap.Toast(document.getElementById("moniteurToast"));
    toast.show();
  }
});

// Charger les moniteurs et remplir le tableau
async function chargerMoniteurs() {
  const moniteurs = await listerMoniteurs();
  const tbody = document.getElementById("moniteursTable");
  tbody.innerHTML = "";

  moniteurs.forEach(m => {
    const tr = document.createElement("tr");
    tr.innerHTML = `
      <td>${m.nomMoniteur}</td>
      <td>${m.prenomMoniteur}</td>
      <td>${m.dateNaissance.split("T")[0]}</td>
      <td>${m.dateEmbauche.split("T")[0]}</td>
      <td>
        <div class="form-check form-switch">
          <input class="form-check-input toggle-activite" type="checkbox" 
                 data-id="${m.idMoniteur}" ${m.activite ? "checked" : ""}>
          <label class="form-check-label">${m.activite ? "Actif" : "Inactif"}</label>
        </div>
      </td>
      <td>
        <i class="bi bi-trash text-danger supprimer" 
           role="button" 
           data-id="${m.idMoniteur}" 
           title="Supprimer"></i>
      </td>
    `;
    tbody.appendChild(tr);
  });
}

//  Charger au démarrage
chargerMoniteurs();

//  supprimer moniteur
document.getElementById("moniteursTable").addEventListener("click", async (e) => {
  if (e.target.classList.contains("supprimer")) {
    const id = e.target.dataset.id;
    if (confirm("Supprimer ce moniteur ?")) {
      try {
        await supprimerMoniteur(id);
        await chargerMoniteurs();
      } catch (err) {
        alert("Erreur suppression : " + err.message);
      }
    }
  }
});

//   toggle activité modifier statut)
document.getElementById("moniteursTable").addEventListener("change", async (e) => {
  if (e.target.classList.contains("toggle-activite")) {
    const id = e.target.dataset.id;
    const nouvelleActivite = e.target.checked; // true ou false

    try {
      await modifierActivite(id, nouvelleActivite);
      // mettre à jour le label à côté du switch
      e.target.nextElementSibling.textContent = nouvelleActivite ? "Actif" : "Inactif";
    } catch (err) {
      alert("Erreur lors de la mise à jour : " + err.message);
      // rollback si erreur
      e.target.checked = !nouvelleActivite;
    }
  }
});
