   M S B A M L   `   `   `  ����F  BRollingBall, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:RollingBall.PagesRollingBall.Pages  5      %    RollingBall.Pages.Settings_page   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:RollingBall.Pages     5        N� Title$  Settings_page��5	       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $, &..\Resources\Resources_dictionary.xaml?�6%   %    5      .��5      � ��5   
   �� 5       �� ImageSource$! ..\Resources\background.jpg��6    � ColumnDefinitions 5   
   �� 5      $��3* �6   �� 5      $��2* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      $	��150 �6   �� 5      �� 5      �� 5       $��2* �6   .+�5"   
   (� $��0��6   $��3��6   .��5#      ��    Viewbox_header#���   6   
Settings6K   5$      (� 5%   
   $��1��6   $��0��6   .��5&      ��   Text#���  6   Window size6D   5'      (� 5(   
   $��2��6   $��0��6   .��5)      �� #���  6   Ball6=   5*      �� 5,   
   -   5         ��Name$ x��5,      $��1��6   $��1��6*     textbox#���  6g   .��6�   8006�   �� 5-   
   -   5        $ y��5-      $��1��6   $��2��6*   #���  6h   .��6�   4506�   �� 5/   
   -   5         ��Name$ 
balls_list��5/      $��2��50      $��1��6    $��2��60   ��51      �� 	 �� Path$	 balls_panel_item_source�6J   �� 53   
   -   5        
 ��Name$
 	ok_button��53      $��3��6$   $��3��61     MenuButtonStyle#���  54      $��Center=�57      $��Center=�58      ��55      ��  �� ElementName$ x��$	 ActualHeight���56      �� $ x��$	 ActualWidth�.��59   )   Ok6-   5:      5;      