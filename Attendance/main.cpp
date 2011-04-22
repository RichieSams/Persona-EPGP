#include "XMLreader.h"
#include <fstream>

using namespace std;

int main(int argc, char* argv[])
{
	XMLNode * namePtr;
	XMLNode * partyPtr;
	XMLNode * membersPtr;
	string tempName;
	string raid [20];
	string tempArray [20];
	string tempString;
	string config[9];
	string cmdString;
	int tempInt;
	int x = 0;
	int a = 0;

	XMLRdr XMLDoc("raid.xml");

	XMLNode * rootPtr = XMLDoc.GetRootNode();
	XMLNode * partiesPtr = rootPtr->GetFirstChild();
	partiesPtr = rootPtr->GetNextChild();
	
	for (int i = 0; i < 4; i++)
	{
		partyPtr = partiesPtr->GetNextChild();
		membersPtr = partyPtr->GetNextChild();
		namePtr = membersPtr->GetFirstChild();
		for (int j = 0; j < 5; j++)
		{
			tempName = namePtr->GetElementValue("Name");
			raid [x] = tempName;
			namePtr = membersPtr->GetNextChild();
			x++;
		}	
	}
	
	for (int i = 0; i < 19; i++)
	{
		tempString = raid [i];
		tempInt = -1;
		for (int j = i + 1; j < 20; j++)
		{
			
			if (raid [j] < tempString)
			{
				tempString = raid [j];
				tempInt = j;
			}
		}
		if (tempInt == -1)
		{
			tempString = raid [i];
			tempInt = i;
		}
		raid [tempInt] = raid[i];
		raid [i] = tempString;
	}

	remove ("filesToBeUploaded\\attendance.csv");
	ofstream csvfile("filesToBeUploaded\\attendance.csv");

	for (int i = 0; i < 20; i++)
	{
	csvfile<<raid [i]<<","<<endl;
	}

	csvfile.close();

	ifstream configfile("config.txt");
	while(a < 9)
	{
		getline(configfile,config[a]);
		a++;
    }

	remove("upload.cmd");
	ofstream cmdfile("upload.cmd");
	cmdfile<<"java "<<"-jar "<<"\""<<config[1]<<"\\google-docs-upload-1.4.6.jar\" "<<"\""<<config[1]<<"\\filesToBeUploaded\" "<<"--username "<<config[4]<<" --password "<<config[7]<<" --replace-all";
	cmdfile.close();

	system("upload.cmd");



	return 0;
}

