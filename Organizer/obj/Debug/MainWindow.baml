   M S B A M L   `   `   `  ����D  @Organizer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Organizer	Organizer  5   	       Organizer.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Organizer     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5	      $   Organizer.EnumBooleanConverter   EnumBooleanConverter��%   &	          5
   
   6%   5      .��5      � .+�5   
   �� $��Output Directory��6   $
��Left=�6+   $��	30,29,0,0q�6F   $	��Top=�6Y   �� 5   
   -   5        $��23��5   ?   $��	0,33,28,0q�6K   $��2q�6^    �� TextWrapping$
 Wrap=�6j   $	��Top=�6�   $��Right=�6�   $	��604��6�   ��6~   �� *OutputDirectory+6�   &   System.Windows.Controls.DataGrid  5   
   -   5           
IsReadOnly .    AutoGenerateColumns .   J� 	AllowDrop . $
��Left=�5   �   $	��250��6�   $��
30,107,0,0q�6�   $	��Top=�6�   $	��734��6�   ��6%   �� *FileInfoList+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	File Name��6&   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *
FileName+  5      $ 	Full Path��6%    68   �� *
FullPath+  5      $ 
Created At��6%    69   �� *	Created+5      �� 5   
   $
��Left=�6   $��
30,382,0,0q�6/   $
��Wrap=�6C   $��Sort by��6W   $	��Top=�6f   	� 5   
   $��Year-Month: 1998-04��6   $
��Left=�64   $��
89,383,0,0q�6O   $	��Top=�6c   	 d� 	IsChecked	 5      �� 
 �� Path$
 SortType� �� 	Converter# [   �� ConverterParameter$ 
YearMonthF���� 5   
   -   5        $��ExecuteButton��5   #   $��Execute��6   $
��Left=�6T   $��689,383,0,0q�6o   $	��Top=�6�   $��75��6�    J� RenderTransformOrigin$ 0.293,0.579&�6�   5      5      