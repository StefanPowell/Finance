import matplotlib.pyplot as plt
import csv
import pandas

df = pandas.read_csv('tilray_jan_208_2019.csv', index_col='Name')
print(df)



#def get_data(data):


#function get closing price
#get each line
#pass over first line - has heading
#each line value four should be stored in an array

#function get date, similar, point 0 used

x = [2, 4, 6]
y = [1, 3, 5]
plt.plot(x, y)
plt.show()
