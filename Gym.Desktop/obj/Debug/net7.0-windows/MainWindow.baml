   M S B A M L   `   `   `  ����  Gym.Desktop    Gym.Desktop.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � Title$  
MainWindow��5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   $	��650��5   	   $
��1000��5   	   
������  � AllowsTransparency$
 True��5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��* �6   .+�5   
   ��    PrimaryBackground#���   6   $��0��5        PrimaryBorderColor#���  5      $��0 0 0 1q�5      ._�5      � $��10 0 0 0q�6    � ColumnDefinitions 5      �� 5       $��* �6'   �� 5!      $	��105 �6'   .+�5&      �� -   5         ��Name$ brDrager��5(      $��0��5&      #���   5'       � Cursor$ 	ScrollAll��5*      5+      �� 5.      $��1��6   #���   5/      	 �� Orientation$	 
Horizontal=�50      .+�52      �� -   5        
 ��Name$
 btnMinimize��52        MinimizeButton#���  6L   �� 53      -   5        $
 btnMaximize��53        MaximizeButton#���  6L   �� 54      -   5        $
 btnExit��54        
ExitButton#���  6D   56      58      5:      � 5=   
   $��1��6    5?      �� 5@      $	��250 �6#   �� 5A      $��* �6#   .+�5E      �� $��0��6   #���   5F      ._�5H      � $��0 20 0 0q�6   .+�5J      �  5K      �� 5L      $��* �6,   �� 5M      $��40 �6,   .+�5Q      �� $��0��6%   .+�5R      �� $��60��6%   $��	0 20 0 20q�5S   %   $��Center=�5T   %   ._�5U   "   �� $!��Assets/Images/logo-icon.png��6(   5V      	� 5W      -   5         	�Name$ rbDashboard��5W   *   $��	Dashboard��5X   *    d� 	IsChecked$
 TrueY�5Y   *     
MenuButton#���  5Z   *     dashboard-icon
 � Tag# �  5\   *   	� 5]      -   5        $ 
rbPackages��5]   *   $��Packages��5^   *   #���  5_   *     package-icon# �  5a   *   	� 5b      -   5        $ 	rbClients��5b   *   $��Clients��5c   *   #���  5d   *     	user-icon# �  5f   *   	� 5g      -	   5        $ rbInstructors��5g   *   $��Instructors��5h   *   #���  5i   *    	 instructor-icon# � 	 5k   *   	� 5l      -
   5        $ 	rbMembers��5l   *   $��Memberships��5m   *   #���  5n   *    
 member-icon# � 
 5p   *   	� 5q      -   5        $ 
rbPayments��5q   *   $��Payments��5r   *   #���  5s   *     payment-icon# �  5u   *   	� 5v      -   5        $ rbFAQ��5v   *   $	��FAQ��5w   *   #���  5x   *     faq-icon# �  5z   *   	� 5{      -   5        $ rbAbout��5{   *   $��About��5|   *   #���  5}   *     
about-icon# �  5   *   5�      �� 5�      $��1��6!   $
��Left=�5�   !   $��20 0 0 0q�5�   !   ._�5�      �� -   5         ��Name$ switcherTheme��5�   '   $��Center=�5�   '   $��Center=�5�   '     SwitchThemeButton#���  5�   '   5�      5�      5�      5�      5�      �� 5�        SecondaryBackground#���  6   $��1��5�      ._�5�       � -   5          �Name$ PageNavigator��5�        � NavigationUIVisibility$ Hidden=�5�      5�      5�      5�      5�      