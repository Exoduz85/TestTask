# Code project
Test Task - Unity Developer

**Objective:**

Develop a Unity project demonstrating your skills in 3D object creation, animation, and interaction within the Unity environment, with an optional AR/VR integration as a bonus.

## Setup Instructions
No specific setup is required. Simply load the sample scene in the Unity Editor and hit play to start the application.

## Description of the Implementation
This project demonstrates the implementation of a feature using two approaches: compute shaders and "traditional" MonoBehaviours. 

The design follows a clear separation of concerns, adhering to the principle of single component - single responsibility. Each specific task is encapsulated within its own dedicated component, allowing for modular and easily understandable code.

While implementing this feature, the following decisions and shortcuts were made:
- Magic numbers were used in some areas to save time, but an effort was made to structure the code to minimize excess complexity.
- Key fields were left public to allow for straightforward adjustments, though safety validations were not included.
- The simplicity of implementation was prioritized in some areas over extensibility, particularly in tasks such as spawning or configuring objects.

Since I don't own any VR equipment I opted for a mouse integration for the hand tracking bonus where you can click and hold the left or right mouse button to attract object A or B.
The object attraction has a depth limiter so that the object is not attracted towards the user in the Z plane.

## Assumptions
1. It was not necessary to create separate setup scriptable objects for configuring objects A and B.
2. Public fields were left without safety mechanisms, assuming that direct modifications would not cause unintended consequences.
3. Objects A and B were not set up as prefabs but instead initialized directly within the Object Spawner component.
4. When the mouse interacts with objects A or B (attraction), the Lissajous animation should pause.
5. The use of Unity Jobs or Burst Compiler was not a requirement. However, it would be possible to add such optimizations by transforming managed objects and data into native objects compatible with Unity's job system and burst compilation.
6. That it was okay to add the third dimension (Z) to the Lissajous calculations. 


## **Task Requirements**

### **1. Procedural Mesh Creation**
- Procedurally create a 3D mesh with distinguishable front and back sides. (Example: A sphere as the main body with a cone in front.)
- Assign the mesh to a new GameObject named Object A.
- Add all required components for rendering (e.g., MeshFilter, MeshRenderer, and a material).

### **2. Secondary Object**
- Create another simple 3D model (e.g., a sphere) named Object B.
- Ensure it is distinguishable in appearance from Object A.

### **3. Lissajous Animation**
- Animate both Object A and Object B using Lissajous curves:
	- Use different parameters for the Lissajous formulas to make their movements appear aperiodic or random.
	- Example Lissajous formula:
		-x(t) = A sin(at + δ) and y(t) = B sin(bt + γ)

### **4. Object Rotation**
- Program Object A to rotate towards Object B at a given angular speed.

### **5. Color Change Based on Angle**
- Dynamically change the color of Object A based on the angle between its forward vector and the vector pointing from Object A to Object B:
	- Color transitions from red when Object B is in front to blue when Object B is behind.

### **6. Mesh Vertex Animation**
- Animate the vertices of Object A’s body mesh using Perlin noise:
	- Displace the vertices along their normal vectors.
	- The displacement should depend on the Perlin noise value, creating a dynamic, organic effect.