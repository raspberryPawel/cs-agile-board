   M S B A M L   `   `   `  ����I  EAgileBoardView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AgileBoardViewAgileBoardView  5      !    AgileBoardView.EditTaskPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:AgileBoardView     5        N� Title$  EditTaskPage��5	      .��5      � 
�������.+�5   
   �� -   5         ��Name$ lblName��5      $��Nazwa��6!   $
��Left=�61   $��	20,57,0,0q�6L   $	��Top=�6_   $	��115��6w   $��52��6�   $��14(�6�   �� 5   
   -   5        $ lblDescription��5      $
��Opis��6(   $
��Left=�67   $��
20,109,0,0q�6R   $	��Top=�6f   $	��115��6~   $��29��6�   $��14(�6�   �� 5   
   -   5         ��Name$ txtName��5      $
��Left=�6#   $��23��6>   $��
156,57,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��413��6�   �� 5   
   -   5        $ txtDescription��5      $
��Left=�6*   $��71��6E   $��156,109,0,0q�6Q   $
 Wrap=�6f   $	��Top=�6z   $	��413��6�   �� 5   
   -   5         ��Name$ btnEdit��5      $��Edytuj��6"   $
��Left=�63   $��630,349,0,0q�6N   $	��Top=�6c   $	��140��6{   $��61��6�   $��16(�6�   ���	#FF251F1F
�������$��Century Gothic)�6�   �� 5   
   -   5        $ btnBack��5      $��Wróć��6"   $
��Left=�61   $��470,349,0,0q�6L   $	��Top=�6a   $	��140��6y   $��60��6�   ���	#FF251F1F
�������$��16(�6�   $��Century Gothic)�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ pickerDateEnd��5      $
��Left=�6,   $��156,219,0,0q�6G   $	��Top=�6\   $	��120��6t   �� 5   
   -   5        $ 
lblDateEnd��5      $��Data zakończenia��6$   $
��Left=�6?   $��
20,219,0,0q�6Z   $	��Top=�6n   $	��131��6�   $��34��6�   $��14(�6�   �� 5   
   -	   5         ��Name$ cbEstimation��5      $
��Left=�6)   $��156,260,0,0q�6D   $	��Top=�6Y   $	��120��6q   �� 5   
   -
   5        $ lblEstimation��5      $��Estymata��6'   $
��Left=�6:   $��
20,258,0,0q�6U   $	��Top=�6i   $	��115��6�   $��28��6�   $��14(�6�   �� 5   
   -   5        $ cbColumn��5      $
��Left=�6%   $��156,305,0,0q�6@   $	��Top=�6U   $	��120��6m   �� 5   
   -   5        $ 	lblColumn��5      $��Kolumna:��6#   $
��Left=�66   $��
20,299,0,0q�6Q   $	��Top=�6e   $	��115��6}   $��28��6�   $��14(�6�   �� 5   
   -   5        $ lblError��5      $�� ��6"   $
��Left=�6-   $��
17,385,0,0q�6H   $	��Top=�6\   $	��430��6t   $��38��6�   $��14(�6�   
������ $
��Bold"�6�   ���	#FFD100006�   �� 5   
   -   5        $ 
cbAssignee��5      $
��Left=�6'   $��157,345,0,0q�6B   $	��Top=�6W   $	��120��6o   �� 5   
   -   5        $ lblPosition��5      $��	Pracownik��6%   $
��Left=�69   $��
20,341,0,0q�6T   $	��Top=�6h   $	��115��6�   $��28��6�   $��14(�6�   5      5      