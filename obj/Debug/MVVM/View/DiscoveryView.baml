   M S B A M L   `   `   `  ����G  CModernDesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:ModernDesign.MVVM.ViewModernDesign.MVVM.View  5      I)clr-namespace:ModernDesign.MVVM.ViewModelModernDesign.MVVM.ViewModel  5      *    $ModernDesign.MVVM.View.DiscoveryView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:ModernDesign.MVVM.View     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @
viewModels)clr-namespace:ModernDesign.MVVM.ViewModel     5      ���#1B1B1B  � DataContext  5      4  .ModernDesign.MVVM.ViewModel.DiscoveryViewModel  5   
   .��5      � .+�5   
   �� $��0,2,0,0q�5      $
��Left=�5      $	��Top=�5       �� Orientation$ 
Horizontal=�5      .+�5      �� $	��100��5      $��30��5      $��5q�5      $	��Add��5      ��5      �� *
AddCommand+5      �� 5      $	��100��5       $��30��5!      $��5q�5"      $��Delete��5$      ��5#      �� *DeleteCommand+5$      �� 5&      $	��100��5'      $��30��5(      $��5q�5)      $
��Edit��5+      ��5*      �� *EditCommand+5+      5,      &   System.Windows.Controls.DataGrid  5/   
   -   5        $��
dgProducts��50      $��0,44,0,0q�51         AlternatingRowBackground �#2D2D30   AutoGenerateColumns .  ���#2D2D30���#007ACC$��1q�56         ColumnHeaderHeight$ 32Q�57      
�������   GridLinesVisibility$
 None=�59         HorizontalGridLinesBrush �#333337   
IsReadOnly .     RowBackground �#333337	   SelectionMode$	 Single=�5?      
   SelectionUnit$
 FullRow=�5@         VerticalGridLinesBrush �#333337��5<      �� *
Products+ �� SelectedItem 5>      �� *SelectedProduct+ � 	Resources 5B      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%�   '        �� 5C      "  6   .�5D      ��  � �� �#3C3C3F63   �� 5E       � ��
 �����63   �� 5F       � ��$ 32��6/   5G      5H         Columns 5I      0  *System.Windows.Controls.DataGridTextColumn  5J      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5K         Header$ SKU ID��5M      1  +System.Windows.Controls.DataGridBoundColumn   Binding 5L      �� *SKU_ID+5M        5N      $ * 5O      $
 Name��5Q       5P      �� *Name+5Q        5R      $ * 5S      $ Price��5U       5T      �� *Price+5U        5V      $ * 5W      $ Description��5Y       5X      �� *Description+5Y        5Z      $ * 5[      $ Quantity��5]       5\      �� *
Quantity+5]        5^      $ * 5_      $ Category��5a       5`      �� *
Category+5a      5c      5d      5e      