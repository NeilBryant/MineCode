/**
 * Copyright (c) 2015 Entertainment Intelligence Lab.
 * Last edited by Matthew Guzdial 06/2015
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using System.Collections;

/**
 * TerrainGenerator is a child of Generator that handles creating a randomized terrain-like environment. 
 */
public class TerrainGenerator: Generator {
	private const int LAND_SIZE = 40;//The radius of the chunk of land this Terrain generates

	//All the perlin noise generators
	private PerlinNoise2D noise1 = new PerlinNoise2D (1 / 150f, new Vector2 (53.1f, -85.8f));
	private PerlinNoise2D noise2 = new PerlinNoise2D(1/150f, new Vector2(96.2f, 50.3f));
	private PerlinNoise3D noise3d;

	public TerrainGenerator(){
		double[] values = new double[]{0.4509611, 0.7773679, 0.3903314, 0.113022, 0.7033318, 0.5206975, 0.8149701, 0.2739967, 0.377223, 0.03645039, 0.6489916, 0.9504886, 0.888083, 0.4379272, 0.7617954, 0.01397753, 0.9920754, 0.1349456, 0.09174395, 0.7701716, 0.6634083, 0.04010308, 0.3421368, 0.5690462, 0.9310497, 0.475233, 0.3826113, 0.0959474, 0.2416303, 0.002116919, 0.793354, 0.1447756, 0.6243004, 0.4998934, 0.6565034, 0.8695886, 0.5364249, 0.8247738, 0.4721088, 0.6777615, 0.7342876, 0.4527584, 0.04462839, 0.009242178, 0.46082, 0.3692358, 0.4404144, 0.2448358, 0.5455244, 0.8541789, 0.3139704, 0.8707908, 0.09771182, 0.3498465, 0.5676764, 0.1103195, 0.3828279, 0.9724042, 0.5755488, 0.3541023, 0.5071647, 0.8568001, 0.0878892, 0.9266615, 0.2250212, 0.6813223, 0.2366791, 0.1869412, 0.6847555, 0.4875249, 0.1907877, 0.6442153, 0.3760758, 0.866846, 0.2975649, 0.4077615, 0.7827343, 0.9525133, 0.9119281, 0.3673666, 0.6823246, 0.6502338, 0.2558978, 0.5682008, 0.2368836, 0.3167103, 0.9487535, 0.4233723, 0.8545856, 0.712423, 0.01343799, 0.3878202, 0.5378482, 0.9476615, 0.3167106, 0.2898225, 0.183171, 0.3860207, 0.8970317, 0.5320386, 0.3929991, 0.3970609, 0.1098591, 0.3104832, 0.2107239, 0.2675501, 0.8177066, 0.907057, 0.9467438, 0.3122553, 0.8880929, 0.1060079, 0.8594784, 0.8221202, 0.4346883, 0.5814888, 0.3985407, 0.3269691, 0.2004237, 0.3141518, 0.6023197, 0.04174173, 0.09524347, 0.4416685, 0.9605103, 0.9633414, 0.335729, 0.03620828, 0.9512711, 0.5911935, 0.5035984, 0.2465422, 0.6624438, 0.9460003, 0.3114779, 0.5915158, 0.4944902, 0.8465086, 0.7990158, 0.3068587, 0.3732031, 0.9536197, 0.610964, 0.05677057, 0.9999268, 0.4973769, 0.3157884, 0.8166069, 0.2866444, 0.353216, 0.9547226, 0.7967895, 0.4564463, 0.8843549, 0.8049586, 0.1947844, 0.4291916, 0.3471391, 0.09592368, 0.8543749, 0.3433329, 0.7041551, 0.586894, 0.8080976, 0.5434607, 0.2608106, 0.1053803, 0.5779516, 0.7372597, 0.339594, 0.9276166, 0.3691824, 0.3132843, 0.972478, 0.9748574, 0.5263558, 0.3411763, 0.8216913, 0.3268042, 0.09883143, 0.3411677, 0.01137149, 0.7340073, 0.04663229, 0.974427, 0.3480197, 0.3682905, 0.09301055, 0.04135168, 0.2533332, 0.2889363, 0.7230324, 0.2116137, 0.4374341, 0.04735745, 0.9782459, 0.4126388, 0.09996571, 0.6391092, 0.3468, 0.06144572, 0.7667269, 0.4444824, 0.2987672, 0.3728156, 0.1490888, 0.7195756, 0.007476927, 0.6200797, 0.6152714, 0.7166216, 0.120652, 0.1620965, 0.543339, 0.7514929, 0.171404, 0.9339439, 0.5524563, 0.4193994, 0.8152484, 0.1384792, 0.3274173, 0.3602177, 0.8795792, 0.631864, 0.5669954, 0.4296944, 0.8141296, 0.7449469, 0.7028691, 0.5786027, 0.5801895, 0.7370556, 0.5211276, 0.4234285, 0.7775999, 0.4453518, 0.9815862, 0.2461757, 0.6528264, 0.7694696, 0.4448327, 0.7775905, 0.1540802, 0.6138043, 0.9057682, 0.7339018, 0.6894183, 0.9372724, 0.01388657, 0.2977304, 0.1161649, 0.3583924, 0.3901919, 0.1645464, 0.5804722, 0.3998006, 0.7696404, 0.2666973, 0.9732393, 0.4389157, 0.4364658, 0.6510566, 0.8662932, 0.5860874, 0.8470176, 0.08477116, 0.7402862, 0.7225738, 
			0.7320326, 0.02274561, 0.5424795, 0.2207792, 0.09662248, 0.885698, 0.6734832, 0.7699534, 0.8802918, 0.559356, 0.256579, 0.3045837, 0.5737866, 0.423227, 0.9633018, 0.6502431, 0.9233561, 0.5444775, 0.06743205, 0.9284189, 0.4581543, 0.3892949, 0.5400077, 0.9334836, 0.5128104, 0.8098092, 0.436021, 0.2902746, 0.753754, 0.3087867, 0.1548529, 0.7947681, 0.03087807, 0.8276678, 0.5781025, 0.03337253, 0.07955719, 0.6804689, 0.9559656, 0.737163, 0.620926, 0.8527597, 0.3925799, 0.6823311, 0.5681763, 0.4029546, 0.188417, 0.3291618, 0.1958721, 0.6846167, 0.3064019, 0.7141392, 0.4631012, 0.2743661, 0.2049241, 0.7699946, 0.7568401, 0.4942157, 0.8398663, 0.5694256, 0.09676303, 0.9350917, 0.8837919, 0.7934356, 0.4269989, 0.08456565, 0.7822269, 0.08550859, 0.2614978, 0.3850853, 0.4930741, 0.6867535, 0.2767211, 0.637534, 0.1150823, 0.5624356, 0.3571569, 0.4618502, 0.1115132, 0.3520822, 0.9273106, 0.5720605, 0.6664439, 0.5261837, 0.06201292, 0.3904949, 0.7529274, 0.2181212, 0.8463131, 0.9156922, 0.8015819, 0.004623533, 0.1903398, 0.152889, 0.7571323, 0.1156987, 0.01262915, 0.8193754, 0.8599553, 0.1330736, 0.9045616, 0.2773487, 0.9643581, 0.8771092, 0.0256393, 0.657998, 0.4028226, 0.8819809, 0.9124508, 0.7722616, 0.03969813, 0.9478865, 0.2052846, 0.5398886, 0.05540384, 0.9235824, 0.3007562, 0.5897322, 0.9261484, 0.2295413, 0.2355402, 0.9038041, 0.6174894, 0.07056046, 0.4368431, 0.02813161, 0.9159619, 0.3471368, 0.4973894, 0.2459282, 0.4150966, 0.2129174, 0.8372934, 0.6844618, 0.8012763, 0.932448, 0.8520837, 0.6964239, 0.4811411, 0.2020837, 0.4703398, 0.3061362, 0.8551601, 0.987287, 0.8174621, 0.05484987, 0.7749298, 0.4698626, 0.3097906, 0.2782797, 0.827824, 0.9600972, 0.6493252, 0.4573881, 0.2550128, 0.3686529, 0.4542573, 0.006585957, 0.597562, 0.06078959, 0.9309249, 0.2635867, 0.5721183, 0.09341503, 0.3059116, 0.3513251, 0.942571, 0.9046963, 0.5427929, 0.7870451, 0.05888034, 0.5425147, 0.8509085, 0.1913592, 0.1881956, 0.6818445, 0.2234162, 0.743638, 0.1743423, 0.8405595, 0.02285493, 0.2992832, 0.2329345, 0.1844696, 0.6388469, 0.0126487, 0.8489101, 0.7523632, 0.8987402, 0.852941, 0.4537738, 0.7160682, 0.9439669, 0.9858102, 0.3019941, 0.1477057, 0.7374606, 0.5539677, 0.6187506, 0.0172143, 0.2888701, 0.3423003, 0.3430463, 0.3191514, 0.04963995, 0.20952, 0.1699485, 0.3266583, 0.1527449, 0.4821095, 0.1059231, 0.5274655, 0.2176444, 0.7804652, 0.6251413, 0.3565861, 0.3242567, 0.1561705, 0.714906, 0.2755947, 0.1447977, 0.5290672, 0.7434685, 0.3274885, 0.01081777, 0.734961, 0.2727204, 0.8189788, 0.9645079, 0.1970245, 0.6377719, 0.7379608, 0.7164496, 0.8447224, 0.3547212, 0.8090981, 0.6260758, 0.6982262, 0.7242427, 0.5263712, 0.004584551, 0.9667231, 0.7087484};
		noise3d = new PerlinNoise3D (1 / 30f, values);
	}
	/**
	 * Call this to generate out the full terrain.
	 */
	public override void GenerateMap (){
		Vector3 cameraPos = Camera.main.transform.position;
		base.GenerateMap ();

		//Block stoneBlock = Map.Instance.GetBlockByIndex (11);
		for (int i = -1*LAND_SIZE; i<=LAND_SIZE; i++) {
			for(int j = -1*LAND_SIZE; j<=LAND_SIZE; j++){
				if(Mathf.Abs(i)>=LAND_SIZE-3 || Mathf.Abs(j)>=LAND_SIZE-3){
					GenerateBlock((int)(cameraPos.x+i),LAND_SIZE-Mathf.Max(Mathf.Abs(i),Mathf.Abs(j)),(int)(cameraPos.z+j),0);
				}
				else{
					Generate ((int)(cameraPos.x+i), (int)(cameraPos.z+j));
				}
			}
		}


		//Door insert
		for(int xPlus = 0; xPlus<2; xPlus++){
			for(int yPlus = 0; yPlus<3; yPlus++){
				MapBuilderHelper.BuildBlock ("Door", -4+xPlus, 6-yPlus,-25 );
			}
		}


	}	

	//Helper method to generate out a single x,y section of the terrain 
	private void Generate(int cx, int cz) {
		int h1 = (int) (noise1.Noise(cx, cz)*4);
		if(h1<0){
			h1*=-1;	
		}

		h1 = (h1)<3? 3: (h1);
		h1 = (h1)>8? 8 : h1;
		
		int h2 = (int) (noise2.Noise(cx, cz)*2);
		h2 = h2<0 ? 0: h2;
		h2 = 6>h2 ? 6: h2;
		h2 += h1;
		
		int deep = 0;
		int worldY = h2;
		for(; worldY>h1; worldY--) {
			if(noise3d.Noise(cx, worldY, cz) < 0) {
				GenerateBlock(cx, worldY, cz, deep);
				deep++;
			} else {
				deep = 0;
			}
		}

		for(; worldY>=0; worldY--) {
			GenerateBlock(cx, worldY, cz, deep);
			deep++;
		}
	}

	//Helper method to generate a specific block for a given distance
	private void GenerateBlock(int worldX, int worldY, int worldZ, int deep) {
		string block = GetBlock(worldX, worldY, worldZ, deep);
		if(block != null) MapBuilderHelper.BuildBlock (block, worldX, worldY,worldZ );
	}

	//Helper method to determine what Block to use based on depth
	private string GetBlock(int worldX, int worldY, int worldZ, int deep) {
		if(deep == 0) return "Grass";
		if(deep <= 5) return "Dirt";
		return "Stone";
	}

}


