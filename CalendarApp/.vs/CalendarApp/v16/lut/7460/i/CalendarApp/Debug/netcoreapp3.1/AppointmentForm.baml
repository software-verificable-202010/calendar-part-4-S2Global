   M S B A M L   `   `   `  ����F  BCalendarApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:CalendarAppCalendarApp  5   	   !    CalendarApp.AppointmentForm   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:CalendarApp     5   	   X TXceed.Wpf.Toolkit, Version=4.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  61     � Title$  AppointmentForm��5   	   $	��300��6!   $	��300��6.   .��5	      � $��10q�6    � ColumnDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��5,5,5,5q�6   .��6"   Title:6*   �� 5   
   -   5         ��Name$ titleBox��5   3   $��1��6   $��0,5,0,5q�6"   63   �� 5   
   $��5,5,5,5q�6   $��1��6!   .��6.   Start Date:6;   &   Xceed.Wpf.Toolkit.DateTimePicker  5   
   -   5          Name$ startDateBox��5   L   $��1��6   $��1��6+   $��0,5,0,5q�6;   6L   �� 5   
   $��5,5,5,5q�6   $��2��6!   .��6.   	End Date:69     5   
   -   5        $ 
endDateBox��5   L   $��2��6   $��1��6+   $��0,5,0,5q�6;   6L   �� 5   
   $��5,5,5,5q�6   $��3��6!   .��6.   Participants:6=   �� 5   
   -   5        $ participantBox��5   @   $��3��6   $��1��6   $��0,5,0,5q�6/    � ToolTip$G ASeparate each username with a space. Creator added automatically.��6X   �� 5   
   $��5,5,5,5q�6   $��4��6!   .��6.   Description:6<   �� 5   
   -   5        $ descriptionBox��5   @   $��4��6   $��1��6   $��0,5,0,5q�6/   6@   �� 5    
   -   5         ��Name$ submitButton��5    m   $��Submit��6   $��5��6"   $��1��6/   $��Right=�6?   $��0,10,0,0q�6[   6m   �� 5!   
   -   5        $ deleteButton��5!   �   $��Delete��6   $��5��6"   $��0��6/   $
��Left=�6?   $��0,10,0,0q�6Z   
����p��6�   5"      5#      