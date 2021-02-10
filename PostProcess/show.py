import cv2

x = cv2.namedWindow("image")
fname = 'image_00000_img.png'
img = cv2.imread(fname)

cv2.imshow('image',img)
#add wait key. window waits till user press any key
cv2.waitKey(0)
#and finally destroy/Closing all open windows
cv2.destroyAllWindows()

