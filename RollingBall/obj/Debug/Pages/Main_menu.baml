   M S B A M L   `   `   `  ����F  BRollingBall, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RollingBallRollingBall  5      a ]PresentationFramework.Aero, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35gJclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.AeroMicrosoft.Windows.Themes 5          RollingBall.Main_menu   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:RollingBall     5      ]ThemesJclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero     5        N� ShowsNavigationUI  .   N� Title$ 	Main_menu��5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $, &..\Resources\Resources_dictionary.xaml?�6%   %    5      .��5      � ��5   
   �� 5       �� ImageSource$! ..\Resources\background.jpg��6   .+�5   
   � $��10,0,0,0q�6    � ColumnDefinitions 5      �� 5      $
��0.3* �6#   62   �� 5      $
��0.4* �6#   62   �� 5      $
��0.3* �6#   62    � RowDefinitions 5       �� 5!      $
��0.3* �6    �� 5"      $
��0.2* �6    �� 5#      $
��0.2* �6    �� 5$      $
��0.2* �6    �� 5%      $
��0.2* �6    �� 5&      $
��0.2* �6    �� 5'      $
��0.1* �6    .+�5)      (� $��0��6   $��3��6#   .��5*      ��    Viewbox_header#���   6   RollingBall6R   5+      �� 5,      -   5        $��Arcade_button��5,      $��1��5.      $��1��6%     MenuButtonStyle#���  62   .��6\   Arcade50      �� 51      -   5         ��Name$ Challenge_button��51      $��1��53      $��2��6!   #���  6.   .��6X   	Challenge6c   �� 54      $��1��6   $��3��6%   #���  62   .��6\   Custom(work in progres)6u   �� 55      -   5        $��Settings_button��55      $��1��56      $��4��6%   #���  62   .��57   3   
Settings59      �� 5:      -   5        $��Quit_button��5:      $��1��5;      $��5��6%   #���  62   .��5<   /   Quit5>      5?      5@      5A      