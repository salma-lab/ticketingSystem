import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Utilisateur, CreateUtilisateurDTO } from '../models/utilisateur.model';
import { Ticket } from '../models/ticket.model';

@Injectable({
  providedIn: 'root'
})
export class UtilisateurService {
  private apiUrl = 'https://localhost:7004/api/utilisateurs'; // Adjust the URL as needed

  constructor(private http: HttpClient) {}

  // Retrieve all utilisateurs
  getAllUtilisateurs(): Observable<Utilisateur[]> {
    return this.http.get<Utilisateur[]>(this.apiUrl);
  }

  // Get a single utilisateur by ID
  getUtilisateurById(id: number): Observable<Utilisateur> {
    return this.http.get<Utilisateur>(`${this.apiUrl}/${id}`);
  }

  // Create a new utilisateur
  createUtilisateur(utilisateurData: CreateUtilisateurDTO): Observable<Utilisateur> {
    return this.http.post<Utilisateur>(this.apiUrl, utilisateurData);
  }

  // Update an existing utilisateur
  updateUtilisateur(id: number, utilisateurData: CreateUtilisateurDTO): Observable<Utilisateur> {
    return this.http.put<Utilisateur>(`${this.apiUrl}/${id}`, utilisateurData);
  }

  // Delete an utilisateur by ID
  deleteUtilisateur(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  // Get tickets for a specific utilisateur
  getTicketsForUtilisateur(id: number): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(`${this.apiUrl}/${id}/tickets`);
  }
}
