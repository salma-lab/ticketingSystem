   M S B A M L   `   `   `  ����   WpfAuthenticationApp)    #WpfAuthenticationApp.AdminDashboard   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Admin Dashboard��$	��738��$	��900��
������� � 	Resources  �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value��%%  '��       �� " ��.���  � ��
 ��ح���  � ��
 ��  ���  � ��$
 Bold"���  � ��
 ���F���  � ��$ 1q� P�  �@J�IsMouseOver$
 True��. ���  � ��
 ��·�P�  �@��
IsSelected$
 True��. ���  � ��
 ������  � ��
 �   �.��� $��
0,0,0,-171q�.+��� -   $��10,10,10,169q�
�������.@��� $��Admin��$��
-2,-2,-2,2q�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��* �.+��� $��0��$��0,0,0,10q�$	��300��.@��� $��Type d'intervention��.���� .+�&   System.Windows.Controls.DataGrid  -   $��TypeInterventionDataGrid��	   AutoGenerateColumns$	 False��$	��200��$��5q�
   SelectionMode$
 Single=����� *TypeInterventions+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Type��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	NomType+��  �� Orientation$ 
Horizontal=�$��Center=�$��10q�.+��� $"��Nouveau type d'intervention:��$��Center=�$��0,0,5,0q��� -   $��NewTypeNameTextBox��$	��200��$��0,0,10,0q��� -   $��Ajouter��$��75��$��5q�
������
�������$
��Bold"�
�������� -   $��	Supprimer��$��75��$��5q�
���  ��
�������$
��Bold"�
���  ���� $��Type d'appareil��.���� .+�  -   $��TypeAppareilDataGrid��$	 False��$	��200��$��5q�$
 Single=����� *TypeAppareils+   $ Type d'appareil��$ *  �� *NomTypeAppareil+�� $ 
Horizontal=�$��Center=�$��10q�.+��� $��Nouveau type d'appareil:��$��Center=�$��0,0,5,0q��� -   $��NewTypeAppNameTextBox��$	��200��$��0,0,10,0q��� -   $��Ajouter��$��75��$��5q�
������
�������$
��Bold"�
�������� -	   $��	Supprimer��$��75��$��5q�
���  ��
�������$
��Bold"�
���  ���� $��Etage��.���� .+�  -
   $��EtageDataGrid��$	 False��$	��200��$��5q�$
 Single=����� *Etages+   $ Nom d'etage��$ *  �� *
NomEtage+�� $ 
Horizontal=�$��Center=�$��10q�.+��� $��Numero d'etage:��$��Center=�$��0,0,5,0q��� -   $��NewEtageNameTextBox��$	��200��$��0,0,10,0q��� -   $��Ajouter��$��75��$��5q�
������
�������$
��Bold"�
�������� -   $��	Supprimer��$��75��$��5q�
���  ��
�������$
��Bold"�
���  ���� $��Emplacement��.���� .+�  -   $��EmplacementDataGrid��$	 False��$	��200��$��5q�$
 Single=����� *Emplacement+   $ Emplacement��$ *  �� *NomEmplacement+�� $ 
Horizontal=�$��Center=�$��10q�.+��� $��Nouveau emplacement:��$��Center=�$��0,0,5,0q��� -   $��NewEmplacementNameTextBox��$	��200��$��0,0,10,0q��� -   $��Ajouter��$��75��$��5q�
������
�������$
��Bold"�
�������� -   $��	Supprimer��$��75��$��5q�
���  ��
�������$
��Bold"�
���  ���� $��Status��.���� .+�  -   $��StatusDataGrid��$	 False��$	��200��$��5q�$
 Single=����� *
Statuses+   $ Status��$ *  �� *	NomStatus+�� $ 
Horizontal=�$��Center=�$��10q�.+��� $��Nouveau status:��$��Center=�$��0,0,5,0q��� -   $��NewStatusNameTextBox��$	��200��$��0,0,10,0q��� -   $��Ajouter��$��75��$��5q�
������
�������$
��Bold"�
�������� -   $��	Supprimer��$��75��$��5q�
���  ��
�������$
��Bold"�
���  ���� $(��"Liste des demandes d'interventions��$��20��$��Bottom=�.��� $��5q� �� $
��Auto ��� $��* �.+�  -   $��TicketDataGrid��$��0��$	 False��$	��200��$��5q�$
 Single=����� *	Tickets+   $ Email��$ *  �� *Email+  $ Motif��$ *  �� *MotifDemande+  $ Statut��$ *  �� *	NomStatus+  $ Date Création��$ *  �� *DateCreation+ �� StringFormat$ {0:yyyy/MM/dd HH:mm}��  $ 	Oralement��$ *  �� *	Oralement+  $ Appareil��$ *  �� *AppareilNom+  $ Étage��$ *  �� *
NomEtage+  $ Emplacement��$ *  �� *NomEmplacement+  $
 Type��$ *  �� *	NomType+4  .System.Windows.Controls.DataGridCheckBoxColumn  $ 	Validated��$ *  �� *Validation1+4  .System.Windows.Controls.DataGridTemplateColumn  $ Actions��$ *    CellTemplate �� .R��� -   $��Details���� $"��Ajouter/Gérer Interventions��$��1��$��10q�.��� $��10q� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $	��150 ��� $	��250 ��� $	��150 ��� $	��250 �.+��� $��Motif:��$��0��$��0��$��Center=��� -    ��Name$ NewTicketMotifTextBox��$��0��$��1��$��5q��� $��
Oralement:��$��1��$��0��$��Center=��� -    ��Name$  NewTicketOralementCheckBox��$��1��$��1��$
��Left=�$��5q��� $��Nom d'Appareil:��$��2��$��0��$��Center=��� -   $! NewTicketAppareilNomTextBox��$��2��$��1��$��5q��� $��Date:��$��2��$��2��$��Center=��� $��Date and Time:��$��2��$��2��$��Center=�(  "System.Windows.Controls.DatePicker  -     Name$ NewTicketDatePicker��$��2��$��3��$��5q��� $��Étage:��$��3��$��0��$��Center=��� -    ��Name$ NewTicketEtageComboBox��$��3��$��1��$��5q� �� DisplayMemberPath$ NomEtage������ *Etages+�� $��Emplacement:��$��3��$��2��$��Center=��� -   $" NewTicketEmplacementComboBox��$��3��$��3��$��5q�$ NomEmplacement������ *Emplacement+�� $��Type Appareil:��$��4��$��0��$��Center=��� -   $ NewTypeAppareilComboBox��$��4��$��1��$��5q�$ NomTypeAppareil������ *TypeAppareils+�� $��Description:��$��4��$��2��$��Center=��� -   $! NewTicketDescriptionTextBox��$��4��$��3��$��5q��� $��Type:��$��5��$��0��$��Center=��� -    $ NewTicketTypeComboBox��$��5��$��1��$��5q�$ NomType������ *TypeInterventions+�� $��Statut:��$��5��$��2��$��Center=��� -!   $ TicketStatusComboBox��$��5��$��3��$��5q�$ 	NomStatus������ *
Statuses+�� $ 
Horizontal=�$��6��$��4��$��Center=�$��10q�.+��� -"   $��Ajouter l'intervention��$	��200��$��5q�
������
��������� -#   $��Supprimer l'intervention��$	��200��$��5q�
���  ��
��������� $��Les utilisateurs��.���� $��10q�.+�  -$   $��UtilisateurDataGrid��$	 False��$	��150��$
��Auto��$��10q�$��Stretch=�$
 Single=����� *Utilisateurss+   $	 Nom��$ *  �� *Nom+  $ Prénom��$ *  �� *Prenom+  $ Email��$ *  �� *Email+  $ Role d'utilisateur��$ *  �� *
RoleName+�� $$��Ajouter / Modifier Utilisateur��$��10q�.��� $��10q� �� $
��Auto ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $
��Nom:��$��0��$��0��$��Center=�$��5q��� -%   $��
NomTextBox��$��0��$��1��$	��200��$��5q�$��5q�
�������$��1q��� $��Prénom:��$��1��$��0��$��Center=�$��5q��� -&   $��PrenomTextBox��$��1��$��1��$	��200��$��5q�$��5q�
�������$��1q��� $��Email:��$��2��$��0��$��Center=�$��5q��� -'   $��EmailTextBox��$��2��$��1��$	��200��$��5q�$��5q�
�������$��1q��� $��Mot de Passe:��$��3��$��0��$��Center=�$��5q�G� -(   $��PasswordBox��$��3��$��1��$	��200��$��5q�$��5q�
�������$��1q��� $��Rôle:��$��4��$��0��$��Center=�$��5q��� -)   $��RoleComboBox��$��4��$��1��$	��200��$��5q�$��5q�
�������$��1q�$
 Role������ *Roles+�� -*   $��Ajouter��$	��200��$��40��$��10q�$��Center=�
������
�������
������$��1q�