#%%
import numpy as np
import pandas as pd
# import concave_hull
import matplotlib.pyplot as plt
from scipy.spatial import ConvexHull
from skimage import io, transform
import warnings, os
from random import randint
import numba, argparse

## Classes inside this list will be ignored
IGNORE_CLASSES = ["Plane Instance"]

"""Choose your suffix."""
# specific pass; 1 _id, 2-_layer, 3 _depth, 4 _normals, 5 flow, -1 for all
SUFFIX = ["_id.png", "_img.png"]

img_path = '/home/prithvi/Documents/git/image_synthesis_using_unity/ImageSynth/captures'
main_df = pd.read_csv('/home/prithvi/Documents/git/image_synthesis_using_unity/results.csv', header=None, error_bad_lines=False)
# main_df = main_df.dropna()

def in_notebook():
    try:
        from IPython import get_ipython
        if 'IPKernelApp' not in get_ipython().config:  # pragma: no cover
            return False
    except (ImportError, AttributeError) :
        return False
    return True


# @numba.njit
def show_landmarks(image, landmarks, mid_pt, simplices):
    """Show image with landmarks"""
    plt.imshow(image)
    plt.scatter(mid_pt[:, 0], mid_pt[:, 1], s=1, marker='*', c='b')
    
    for simplex in simplices:
        plt.plot(landmarks[simplex, 0], landmarks[simplex, 1],  'k-' , linewidth=0.25)
        # print(landmarks[simplex, 0], landmarks[simplex, 1])

    plt.scatter(landmarks[:, 0], landmarks[:, 1], s=0.5, marker='.', c='r')
    plt.pause(0.1)  # pause a bit so that plots are updated
#%%

def check_annots(group_dfs, suffix=SUFFIX[1], n=0):
    """ Annotates all objects in a given image.

    Args:
        group_dfs ([DataFrameGroupBy]): main_df.groupby(df[0])
        n ([int]): nth element, by default first image.
    """
    assert in_notebook(), "Ensure you are using jupyter notebook or vscode-python extension to use this method."
    global img_path
    img_name = list(group_dfs.groups.keys())[n]
    df = group_dfs.get_group(img_name)

    image = io.imread(os.path.join(img_path, img_name+suffix))
    plt.imshow(image)
    for i, row in df.iterrows():
        if row[1] not in IGNORE_CLASSES: # Note: row[0] 
            print(row[1])
            row = row.dropna()
            mid_pt = np.array(row.iloc[2:4], dtype=np.int32).reshape(-1,2)
            plt.scatter(mid_pt[:, 0], mid_pt[:, 1], s=1, marker='*', c='b')
            vertices = np.asarray(row.iloc[4:]).astype('int').reshape(-1,2)
            # vertices = vertices[~np.isnan(vertices)] # .reshape(-1, 2) # remove nans
            imp_vertices = ConvexHull(vertices).simplices
            for vert in imp_vertices:
                plt.plot(vertices[vert, 0], vertices[vert, 1],  'k-' , linewidth=0.1)
            plt.scatter(vertices[:, 0], vertices[:, 1], s=0.025, marker='.', c='r')
            # plt.pause(0.01) 
            # show_landmarks(io.imread(os.path.join(img_path, img_name)), landmarks, mid_pt, simplices)

# %%
if __name__=="__main__":
    global img_path, main_df
    if in_notebook:
        warnings.filterwarnings("ignore")
        # %config InlineBackend.figure_format = 'retina'
        plt.ion() 

        group_dfs = main_df.groupby(main_df[0])
        plt.figure()
        n = randint(0, len(group_dfs)-1); print(n)
        check_annots(group_dfs, n=n)
        plt.show()
# # edgs = concave_hull.alpha_shape(landmarks, 2, True)
# # edgs_mod = edgs.reshape(-1,2)
# for simplex in simplices:
#     print(points[simplex, 0], points[simplex, 1])

# edgs = np.array([list(i) for i in list(edgs)])
# print(len(landmarks), len(edgs))

# %%
