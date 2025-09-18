import { ajouterVehicule, listerVehicules, supprimerVehicule, modifierEtatVehicule } from "./vehiculeService.js";


// Ajouter un véhicule

document.getElementById("vehiculeForm").addEventListener("submit", async (e) => {
  e.preventDefault();

  const formValues = {
    numeroImmatriculation: document.getElementById("numeroImmatriculation").value,
    modeleVehicule: document.getElementById("modele").value,
    etat: document.querySelector("input[name='etat']:checked").value === "true"
  };

  try {
    await ajouterVehicule(formValues);
    await chargerVehicules();
    e.target.reset();

    // Toast succès
    document.getElementById("vehiculeToast").classList.remove("bg-danger");
    document.getElementById("vehiculeToast").classList.add("bg-success");
    document.getElementById("vehiculeToastMessage").textContent = " Véhicule ajouté avec succès !";
    new bootstrap.Toast(document.getElementById("vehiculeToast")).show();

  } catch (err) {
    console.error("Erreur :", err);

    // Toast erreur
    document.getElementById("vehiculeToast").classList.remove("bg-success");
    document.getElementById("vehiculeToast").classList.add("bg-danger");
    document.getElementById("vehiculeToastMessage").textContent = " Erreur : " + err.message;
    new bootstrap.Toast(document.getElementById("vehiculeToast")).show();
  }
});


// Charger véhicules dans le tableau

export async function chargerVehicules() {
  const vehicules = await listerVehicules();
  const tbody = document.getElementById("vehiculesTable");
  tbody.innerHTML = "";

  vehicules.forEach(v => {
    const tr = document.createElement("tr");
    tr.innerHTML = `
      <td>${v.numeroImmatriculation}</td>
      <td>${v.modeleVehicule}</td>
      <td>
        <div class="form-check form-switch">
          <input class="form-check-input toggle-etat" type="checkbox" 
                 data-immatric="${v.numeroImmatriculation}" ${v.etat ? "checked" : ""}>
          <label class="form-check-label">${v.etat ? "Dispo" : "En révision"}</label>
        </div>
      </td>
      <td>
        <i class="bi bi-trash text-danger supprimer" 
           role="button" 
           data-immatric="${v.numeroImmatriculation}" 
           title="Supprimer"></i>
      </td>
    `;
    tbody.appendChild(tr);
  });
}


// Supprimer véhicule

document.getElementById("vehiculesTable").addEventListener("click", async (e) => {
  if (e.target.classList.contains("supprimer")) {
    const immatric = e.target.dataset.immatric;
    if (confirm(`Supprimer le véhicule ${immatric} ?`)) {
      try {
        await supprimerVehicule(immatric);
        await chargerVehicules();
      } catch (err) {
        alert("Erreur suppression : " + err.message);
      }
    }
  }
});

// Modifier état 

document.getElementById("vehiculesTable").addEventListener("change", async (e) => {
  if (e.target.classList.contains("toggle-etat")) {
    const immatric = e.target.dataset.immatric;
    const nouvelEtat = e.target.checked; 

    try {
      await modifierEtatVehicule(immatric, nouvelEtat);
      e.target.nextElementSibling.textContent = nouvelEtat ? "Dispo" : "En révision";
    } catch (err) {
      alert("Erreur mise à jour état : " + err.message);
      e.target.checked = !nouvelEtat; 
    }
  }
});

// Charger dès le départ
chargerVehicules();
