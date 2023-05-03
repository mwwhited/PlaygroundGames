
#define OLC_PGE_APPLICATION
#include <iostream>
#include <olcPixelGameEngine/olcPixelGameEngine.h>

using namespace olc;
using namespace std;

#define Point2d v2d_generic<double>

class ProceduralGeneration : public olc::PixelGameEngine
{
public:
	ProceduralGeneration()
	{
		sAppName = "Procedural Generation";
	}

	bool OnUserCreate() override
	{
		return true;
	}

	bool OnUserUpdate(float fElapsedTime) override
	{
		// called once per frame
		srand(0);

		int planets = (rand() % 10) + 5;
		int h = ScreenHeight();
		int w = ScreenWidth();
		int s = (h > w ? w : h) / 50;

		for (int planetIndex = 0; planetIndex < planets; planets++) {

			int x = rand() % w;
			int y = rand() % h;
			int size = rand() % s;
			Pixel color = Pixel(rand() % 256, rand() % 256, rand() % 256);
			Point2d position = Point2d(x, y);

			DrawCircle(position, size, color);

			cout << "x:" << x << " y:" << y << " s:" << size << endl;
		}

		return true;
	}
};


int main()
{
	ProceduralGeneration proceduralGeneration;
	if (proceduralGeneration.Construct(1024, 768, 1, 1))
		proceduralGeneration.Start();

	return 0;
}