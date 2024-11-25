export interface Ticket {
    ticketId: number;
    description: string;
    dateCreation: Date;
    statusId: number;
    typeInterventionId: number;
    utilisateurId: number;
    comments?: string[];
  }
  