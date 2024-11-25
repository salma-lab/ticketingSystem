export interface Utilisateur {
    utilisateurId: number;
    nom: string;
    prenom: string;
    email: string;
    roleName: string;
    //tickets?: Ticket[]; // Optional: include if you want to see user tickets in one view
  }
  
  export interface CreateUtilisateurDTO {
    nom: string;
    prenom: string;
    email: string;
    password: string;
    roleId: number;
  }
  