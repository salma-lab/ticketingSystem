   M S B A M L   `   `   `  ����  WpfAuthenticationApp(    "WpfAuthenticationApp.UserDashboard   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  User Dashboard��5   	   $	��600��6    $	��800��6-   ���#f5f5f5.��5      � $��10q�6   .+�5   
   �� ���#007ACC$��50��6&    �� CornerRadius$ 5��62   $	��Top=�6C   $��0,0,0,10q�6[   ._�5	      �� $��Fiche d'intervention��6   
�������$��Center=�6G   $��Center=�5
      $��20(�65   $
��Bold"�6C   5      � 5   
   $��0,60,0,0q�6   .+�5      �� -   5        
�������$��1q�5      ���#D3D3D3.@�5      �� $��Nouvelle Demande��6   .��5      � $��10q�6    � RowDefinitions 5      �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5      $
��Auto �6,    � ColumnDefinitions 5      �� 5      $
��Auto �6/   �� 5      $��* �6/   �� 5      $
��Auto �6/   �� 5       $��* �6/   .+�5$      �� $��Motif:��6$   $��0��62   $��0��6?   $��Center=�6O   $��5q�6j   �� 5%      -   5         ��Name$ NewTicketMotifTextBox��5%   "   $��0��6A   $��1��6N   $��5q�6^    � ToolTip$$ Entrez le motif de la demande.��6i   �� 5(      $��
Oralement:��6$   $��1��66   $��0��6C   $��Center=�6S   $��5q�6n   �� 5)      -   5         ��Name$  NewTicketOralementCheckBox��5)   #   $��1��6G   $��1��6T   $
��Left=�6d   $��5q�6   �� 5,      $��Nom d'Appareil:��6$   $��2��6;   $��0��6H   $��Center=�6X   $��5q�6s   �� 5-      -   5        $! NewTicketAppareilNomTextBox��5-   "   $��2��6G   $��1��6T   $��5q�6d   �� 5.      $��Date:��6$   $��2��61   $��2��6>   $��Center=�6N   $��5q�6i   (  "System.Windows.Controls.DatePicker  5/      -   5          Name$ NewTicketDatePicker��5/   %   $��2��6B   $��3��6O   $��5q�6_   �� 52      $��Étage:��6$   $��3��62   $��0��6?   $��Center=�6O   $��5q�6j   �� 53      -   5         ��Name$ NewTicketEtageComboBox��53   #   $��3��6C   $��1��6P   $��5q�6`   	 �� DisplayMemberPath$	 NomEtage��6�   ��6k   �� *Etages+6�   �� 54      $��Emplacement:��6$   $��3��68   $��2��6E   $��Center=�6U   $��5q�6p   �� 55      -   5        $" NewTicketEmplacementComboBox��55   #   $��3��6I   $��3��6V   $��5q�6f   $	 NomEmplacement��6�   ��6q   �� *Emplacement+6�   �� 58      $��Type Appareil:��6$   $��4��6:   $��0��6G   $��Center=�6W   $��5q�6r   �� 59      -   5        $ NewTypeAppareilComboBox��59   #   $��4��6D   $��1��6Q   $��5q�6a   $	 NomTypeAppareil��6�   ��6l   �� *TypeAppareils+6�   �� 5:      $��Nom Demandeur:��6$   $��4��6:   $��2��6G   $��Center=�6W   $��5q�6r   �� 5;      -	   5        $ NewTicketNomDeTextBox��5;   "   $��4��6A   $��3��6N   $��5q�6^   �� 5>      $��Type:��6$   $��5��61   $��0��6>   $��Center=�6N   $��5q�6i   �� 5?      -
   5        $ NewTicketTypeComboBox��5?   #   $��5��6B   $��1��6O   $��5q�6_   $	 NomType��6�   ��6j   �� *TypeInterventions+6�   �� 5B      
 �� Orientation$
 
Horizontal=�6%   $��6��6>   $��4��6K   $��Center=�6_   $��10q�6|   .+�5C      �� -   5        $��Ajouter��5C   %   $	��150��67   $��5q�6C   ���#1E90FF
�������6v   5D      5E      5F      5G      5H      5I      5J      