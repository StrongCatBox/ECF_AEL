import { 
  ajouterLecon, 
  listerEleves, 
  listerMoniteurs, 
  listerModelesVehicules, 
  listerCreneaux 
} from "./leconService.js";

// Charger élèves
async function chargerEleves() {
  try {
    const eleves = await listerEleves();
    console.log("Élèves reçus :", eleves);
    const select = document.getElementById("eleve");
    select.innerHTML = '<option value="">-- Choisir un élève --</option>';

    eleves.forEach(e => {
      const opt = document.createElement("option");
      opt.value = e.idEleve;
      opt.textContent = `${e.nomEleve} ${e.prenomEleve}`;
      select.appendChild(opt);
    });
  } catch (err) {
    console.error("Erreur chargement élèves :", err);
  }
}

// Charger moniteurs
async function chargerMoniteurs() {
  try {
    const moniteurs = await listerMoniteurs();
    const select = document.getElementById("moniteur");
    select.innerHTML = '<option value="">-- Choisir un moniteur --</option>';

    moniteurs.forEach(m => {
      const opt = document.createElement("option");
      opt.value = m.idMoniteur;
      opt.textContent = `${m.nomMoniteur} ${m.prenomMoniteur}`;
      select.appendChild(opt);
    });
  } catch (err) {
    console.error("Erreur chargement moniteurs :", err);
  }
}

// Charger modèles de véhicules
async function chargerModelesVehicules() {
  try {
    const modeles = await listerModelesVehicules();
    const select = document.getElementById("modeleVehicule");
    select.innerHTML = '<option value="">-- Choisir un modèle --</option>';

    modeles.forEach(m => {
      const opt = document.createElement("option");
      opt.value = m.nomModeleVehicule;
      opt.textContent = `${m.nomModeleVehicule} (${m.marque}, ${m.annee})`;
      select.appendChild(opt);
    });
  } catch (err) {
    console.error("Erreur chargement modèles :", err);
  }
}

// Charger créneaux
async function chargerCreneaux() {
  try {
    const creneaux = await listerCreneaux();
    const select = document.getElementById("dateHeure");
    select.innerHTML = '<option value="">-- Choisir un créneau --</option>';

    creneaux.forEach(c => {
      const date = new Date(c);
      const formatted = date.toLocaleString("fr-FR", {
        weekday: "long",
        year: "numeric",
        month: "long",
        day: "numeric",
        hour: "2-digit",
        minute: "2-digit"
      });

      const opt = document.createElement("option");
      opt.value = c; // valeur ISO envoyée au backend
      opt.textContent = formatted;
      select.appendChild(opt);
    });
  } catch (err) {
    console.error("Erreur chargement créneaux :", err);
  }
}

// Soumission du formulaire
document.getElementById("leconForm").addEventListener("submit", async (e) => {
  e.preventDefault();

  const dureeValue = parseInt(document.getElementById("duree").value, 10);
  const unite = document.getElementById("uniteDuree").value;

  // Conversion en minutes 
  const dureeFinale = (unite === "heures") ? dureeValue * 60 : dureeValue;

  const formValues = {
    idEleve: document.getElementById("eleve").value,
    idMoniteur: document.getElementById("moniteur").value,
    ModeleVehicule: document.getElementById("modeleVehicule").value,
    dateHeure: new Date(document.getElementById("dateHeure").value).toISOString(),
    duree: dureeFinale
  };

  try {
    await ajouterLecon(formValues);
    e.target.reset();

    // Toast succès
    document.getElementById("leconToast").classList.remove("bg-danger");
    document.getElementById("leconToast").classList.add("bg-success");
    document.getElementById("leconToastMessage").textContent = " Leçon réservée avec succès !";
    new bootstrap.Toast(document.getElementById("leconToast")).show();

  } catch (err) {
    console.error("Erreur réservation :", err);

    // Toast erreur
    document.getElementById("leconToast").classList.remove("bg-success");
    document.getElementById("leconToast").classList.add("bg-danger");
    document.getElementById("leconToastMessage").textContent = " Erreur : " + err.message;
    new bootstrap.Toast(document.getElementById("leconToast")).show();
  }
});

// Charger toutes les données au démarrage
chargerEleves();
chargerMoniteurs();
chargerModelesVehicules();
chargerCreneaux();
