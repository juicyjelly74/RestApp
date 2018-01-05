Insert Into Menu(ID,Name, Description, Price, Category, Image)
Select '17','Egg Sandwich','Two farm fresh eggs* served on seared Sourdough bread with tomato and mayo and your choice of Fried Apples or Hashbrown Casserole.', 6.10 , 'Breakfast', BulkColumn 
from Openrowset (Bulk 'C:\Users\NastyaGurskaya\Documents\Visual Studio 2015\Projects\RestApp\images\eggsa.png', Single_Blob) as Image