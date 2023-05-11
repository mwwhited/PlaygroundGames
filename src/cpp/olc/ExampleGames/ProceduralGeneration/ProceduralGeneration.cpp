
#define OLC_PGE_APPLICATION
#include <iostream>
#include <random>
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
		Clear(BLUE);
		// called once per frame

		random_device rd;
		mt19937 mt(rd());
		uniform_int_distribution<int> dist(0, 256);

		int planets = (rand() % 10) + 5;
		int h = ScreenHeight();
		int w = ScreenWidth();
		int s = (h > w ? w : h) / 50;

		for (int planetIndex = 0; planetIndex < planets; planets++) {

			int x = dist(mt) % w;
			int y = dist(mt) % h;
			int size = dist(mt) % s;
			Pixel color = Pixel(dist(mt) % 256, dist(mt) % 256, dist(mt) % 256);
			Point2d position = Point2d(x, y);

			DrawCircle(position, size, color);

			//cout << "x:" << x << " y:" << y << " s:" << size << endl;
		}

		return true;
	}
};


int main()
{
	ProceduralGeneration proceduralGeneration;
	if (proceduralGeneration.Construct(512, 480, 2, 2))
		proceduralGeneration.Start();

	return 0;
}