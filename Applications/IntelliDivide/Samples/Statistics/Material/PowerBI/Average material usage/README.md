# Average material usage
![Average material usage](https://github.com/user-attachments/assets/1c01c47e-d0bb-4af5-a0c8-11ddfa0726e0)

<strong> What can the report be used for? </strong>

The report can help to analyze and optimize the use of materials in order to reduce costs. Higher material utilization is directly linked to a reduction in costs, especially for frequently used or particularly expensive materials.

<strong> What is shown? </strong>
The diagram in the top left corner shows the average material usage per material, sorted by total part area. The dark blue line represents the part area, while the bars are colored differently depending on the material usage. Green bars indicate a very good usage ratio, while orange bars indicate a very poor usage ratio. In addition, a blue line shows the number of optimizations in which the material is included.

The diagram in the bottom left-hand corner shows the average material usage per day.

On the right-hand side is a list of all optimizations, each showing the number of parts and material usage.

The diagrams are linked so that when a material is selected in the diagram in the top left-hand corner, the other two diagrams are filtered accordingly.

<strong> What is important? </strong>

The materials in the diagram at the top left are sorted by total area. 

![Materials sorted by total area](https://github.com/user-attachments/assets/1227b58b-f29e-476b-a64b-51c96281aa6d)

This means that the materials shown first in particular should have a high material utilization and be shown in green. If this is not the case, it is worth taking a closer look. Click on the material to adjust the filters accordingly. In the example, the material FUMUBI_15 is interesting. The material utilization is exceptionally low in relation to its use.

![Average of yield by material sorted by parts](https://github.com/user-attachments/assets/121892d8-4dbb-4d91-9924-64f3c0046a05)

There is potential for improvement if the material was used in several optimizations on the same or consecutive days. This is the case in the example on 15.03.2024. It was used in 3 optimizations, each with very poor material utilization. 

 The utilization could probably have been increased here by combining the optimizations. There are several options for this:

	- Use of lot creation in productionManager Advanced
	- Importing the parts lists into the same optimization one after the other
