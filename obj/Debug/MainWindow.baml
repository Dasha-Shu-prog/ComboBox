   M S B A M L   `   `   `  ����A  =Table1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Table1.PropertiesTable1.Properties  5   	   clr-namespace:Table1Table1  5   	   clr-namespace:Table1Table1  5   	   U QInteractiveDataDisplay.WPF, Version=0.0.0.0, Culture=neutral, PublicKeyToken=nullkLclr-namespace:InteractiveDataDisplay.WPF;assembly=InteractiveDataDisplay.WPFInteractiveDataDisplay.WPF 5	   	       Table1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -pclr-namespace:Table1.Properties     5   	   &localclr-namespace:Table1     5   	   #ucclr-namespace:Table1     5   	   [d3Lclr-namespace:InteractiveDataDisplay.WPF;assembly=InteractiveDataDisplay.WPF     5	   	   -   5        $��24(�5
        � Title$  
MainWindow��5   	   $	��900��6   $
��1600��6)    � 	Resources 5      )   #Table1.VisibilityToCheckedConverter    VisibilityToCheckedConverter��%   &	          5   
   6-   5      .��5      � $��Center=�6   $��Center=�5       � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $	��120 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5    
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��1��6;   $��2��6K   .+�5!         Table1.MinMaxGraphControl  -   5          Name$ SliderGraph��5!   $      Table1.MinMaxGraphControl   	MouseDown$ SliderGraph_MouseDown��6E   $	��630��69   6E   �� 5"      -   5         ��Name$ LegendGraph��5"      $%��Показать легенду��6,   $��Center=�6G    d� 	IsChecked$
 TrueY�6i   $��Center=�5#      $��Center=�64   6O   5$         Table1.NumericControl  5&   
   -   5        	  Name$	 	timeStart��5&      
   Text$
 0��6/      MinValue$ 0��68      MaxValue$	 180��6E   $��0��6T   $��1��6d     5'   
   -   5        $	 timeStop��5'      $
 0��6.   $ 0��67   $	 180��6D   $��0��6S   $��2��6c     5(   
   -   5        $	 amplit��5(      $	
 -15��6,   $	 -15��67   $ 15��6F   $��0��6T   $��3��6d   �� 5*   
   $��Время начала��6   $��0��6(   $��1��68     txtBlockStyle#���  5+      �� 5-   
   $��Время конца��6   $��0��6'   $��2��67   #���  5.      �� 50   
   $��Амплитуда��6   $��0��6%   $��3��65   #���  51      �� 53   
   -   5         ��Name$ 	color_box��53      $��0��6'   $��4��67     cmbBoxStyle#���  54      .@�55      ��  �� 
IsSelected . $��Красный��6.   
���""��6@   �� 56      $��
Синий��6   
����  �6+   �� 57      $��Зелёный��6   
��� d �6-   �� 58      $��Оранжевый��6   
��� ���6/   �� 59      $��Фиолетовый��6   
���� ��60   �� 5:      $��Другой цвет��6   ���	#FF34345F61   5;      �� 5=   
   -   5         ��Name$
 plus��5=      $��0��6   $��5��6/   $
��Left=�5>        btnStyle#���  5?      ��5@      �� 5A      .R�5B      � .+�5C      A� 
����ح�6"   �� 5D      $��+��6    $��1,1,1,4q�6,     lblStale#���  5E       5F      5G      5I      �� 5K   
   -	   5        $ minus��5K      $��0��6    $��5��60   $��Right=�5L      #���  5M      ��5N      �� 5O      .R�5P      � .+�5Q      A� 
����ح�6"   �� 5R      $��-��6    $��4,0,4,9q�6,   #���  5S       5T      5U      5W      &   System.Windows.Controls.DataGrid  5Y   
   -
   5          Name$ table��5Y         AutoGenerateColumns . $��2��5Z      $��1��6#   $��5��60      
IsReadOnly .    EnableRowVirtualization .     SelectionMode$ Single=�5[   3     dataGtidStyle#���  5\         CanUserSortColumns .     Columns 5]      0  *System.Windows.Controls.DataGridTextColumn  5_      -   5        
TIME_start��,  &System.Windows.Controls.DataGridColumn   Header$ Время с��5_   9   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   ��  �� Path$ 
Time_start�  5`      -   5        	TIME_stop��$ Время по��5`   8    6J   �� $ 	Time_stop�  5a      -   5        	amplitude��$ Амплитуда��5a   8    6K   �� $ 	Amplitude�  5b      -   5        	color��$ Цвет��5b   4    6B   �� $ Color�5d      �� 5f   
   -   5         ��Name$ graphCanvas��5f      $��1��6&   $��1��63   $��5��6C   $	��700��6T   $	��550��6a   .+�5g      &	   InteractiveDataDisplay.WPF.Chart	  -   5        $��plotter��5g      $	��700��5h      $	��550��6     	  BottomTitle$ Амплитуда, мВ��6,    	  	LeftTitle$ Время, мкс��6H    	  
PlotHeight$	 190Q�5i       	  PlotOriginX$	 -20Q�6$    	  PlotOriginY$ 0Q�66     	  	PlotWidth$  45Q�6F   $��Arial Unicode MS)�5j      $��24(�61   ! 	  LegendContent! 5k      1
  +InteractiveDataDisplay.WPF.LegendItemsPanel
  5l      
������  5m       ( $InteractiveDataDisplay.WPF.LineGraph" d� IsThreeState# �� 	Converter$ �� Mode��%�   &	       7   �� 5n      .R�5o   "   �� $ 
Horizontal=�6-   .+�5p   &   �� " .  6/   �� $ 
Visibility�8#   $$ TwoWay=���5q   +   �� *Description+6K   5t   #   5u      5v      5w      .��5y      � -   5        % �Name$% 	linesPlot��5y      $��Center=�6*   $��Center=�5z      5{      5|      F� 5}      -   5        & F�Name$& 	rightLine��5}   !   
����  �$��3��6T   ��5~      B� 5      $��M 290, 0 L 290, 620 ZC�6$   5�      F� 5�      -   5        $& leftLine��5�   '   
����+��$��3��6X   ��5�      B� 5�      $��M 290, 0 L 290, 620 ZC�6$   5�      5�      5�      5�      