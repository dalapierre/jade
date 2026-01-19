from PIL import Image

data = []

file = open("world_test.txt")
lines = file.readlines()
counter = 0

for line in lines:
    line_data = line.split(',')
    line_data.remove("\n")
    data.append(line_data)

width = len(data)
height = len(data[0])

highest = -1

for i in range(width):
    for j in range(height):
        if float(data[i][j]) > highest:
            highest = float(data[i][j])

img = Image.new( 'RGB', (width, height), "black") # Create a new black image
pixels = img.load() # Create the pixel map
for i in range(width):    # For every pixel:
    for j in range(height):
        s = float(data[i][j]) / highest;
        val = int(s * 255)
        pixels[i,j] = (val, val, val) # Set the colour accordingly
        
img.save("./img.png")
input("press ENTER to exit")