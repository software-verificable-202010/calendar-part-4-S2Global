   M S B A M L   `   `   `  ����F  BCalendarApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:CalendarAppCalendarApp  5   	   #    CalendarApp.AppointmentWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:CalendarApp     5   	     � Title$  Appointment��5   	   $	��300��6   $	��300��6*   .��5	      � -   5         �Name$ AppointmentView��5	      $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��2* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   Title:6*   �� 5   
   $��1��6   
Creator:6,   �� 5   
   $��2��6   Start Date:6/   �� 5   
   $��3��6   	End Date:6-   �� 5   
   $��4��6   Participants:61   �� 5   
   $��5��6   Description:6/   �� 5   
   -   5         ��Name$ 
editButton��5   |   $
��Edit��6   $��6��6    $��1��6-   $��Right=�6=   $��5,0,5,0q�6Y   $��5,5,5,5q�6k   6�   5      5      