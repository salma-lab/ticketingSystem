   M S B A M L   `   `   `  ����   WpfAuthenticationApp.    (WpfAuthenticationApp.TicketDetailsWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Ticket Details��$	��500��$	��600��.���  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��Ticket Details��$��20(�$
��Bold"�$��Center=�$��10q��� $��1��$��10q�$
��Auto=�.���� .+��� $��Email:��$
��Bold"���  �� 
IsReadOnly$
 True������ *SelectedTicket.Email+�� $��
Nom Etage:��$
��Bold"�$��0,10,0,0q��� $
 True������ *SelectedTicket.NomEtage+�� $��Description:��$
��Bold"�$��0,10,0,0q��� ���� *SelectedTicket.Description+�� $��Status:��$
��Bold"�$��0,10,0,0q��� ���� *SelectedTicket.NomStatus+�� $��Motif:��$
��Bold"�$��0,10,0,0q��� ���� *SelectedTicket.MotifDemande+�� $��Date Creation:��$
��Bold"�$��0,10,0,0q��� $
 True������ *SelectedTicket.DateCreation+ �� StringFormat$ yyyy/MM/dd HH:mm���� $��
Oralement:��$
��Bold"�$��0,10,0,0q���  d� 	IsChecked �� *SelectedTicket.Oralement+�� $��
Oralement:��$
��Bold"�$��0,10,0,0q���  �� *SelectedTicket.Validation1+�� $��Type Appareil:��$
��Bold"�$��0,10,0,0q��� $
 True������ * SelectedTicket.NomTypeAppareil+�� $��Emplacement:��$
��Bold"�$��0,10,0,0q��� $
 True������ *SelectedTicket.NomEmplacement+�� $��Appareil Nom:��$
��Bold"�$��0,10,0,0q��� ���� *SelectedTicket.AppareilNom+�� $��Type Intervention:��$
��Bold"�$��0,10,0,0q��� ���� *SelectedTicket.NomType+�� $��Validation Time:��$
��Bold"�$��0,10,0,0q��� $
 True������ *SelectedTicket.ValidationTime+$ yyyy/MM/dd HH:mm����  �� Orientation$ 
Horizontal=�$��2��$��Center=�$��10q�.+��� -   $
��Save��$	��100��$��5q��� -   $��Print��$	��100��$��5q��� -   $��Close��$	��100��$��5q�