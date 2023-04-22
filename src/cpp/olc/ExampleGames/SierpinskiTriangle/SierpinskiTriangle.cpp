
#define OLC_PGE_APPLICATION
#include <iostream>
#include <olcPixelGameEngine/olcPixelGameEngine.h>

#define Point2d olc::v2d_generic<double>

class SierpinskiTriangle : public olc::PixelGameEngine
{
public:
	SierpinskiTriangle()
	{
		sAppName = "Sierpinski Triangle";
	}

	bool OnUserCreate() override
	{
		// Called once at the start, so create things here
		a = Point2d(ScreenWidth() / 2, 0);
		b = Point2d(0, ScreenHeight());
		c = Point2d(ScreenWidth(), ScreenHeight());
		return true;
	}

	bool OnUserUpdate(float fElapsedTime) override
	{
		// called once per frame

		int s = rand() % 3;
		switch (s) {
		case 0:
			curr = (curr + a) / 2;
			break;
		case 1:
			curr = (curr + b) / 2;
			break;
		case 2:
			curr = (curr + c) / 2;
			break;
		}

		Draw(curr, olc::Pixel(rand() % 256, rand() % 256, rand() % 256));

		return true;
	}

private:
	Point2d a;
	Point2d b;
	Point2d c;

	Point2d curr;
};


int main()
{
	SierpinskiTriangle sierpinskiTriangle;
	if (sierpinskiTriangle.Construct(512, 512, 1, 1))
		sierpinskiTriangle.Start();

	return 0;
}