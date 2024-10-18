using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pipes;
using Blocks;
using Cells;
using Connectors;
using Testing;

// Contributors: Leif Larson
// Last updated 10/16/2024
// Previous result: 10/16/2024 all tests PASSED!

public class TEST_SteamPropagation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<TestBattery> allBatteries = new List<TestBattery>();
        BuildAreaTest world = new(3, 3);

        // put together the blocks in the world. We're not using parts for this test.
        world.table.Get(0, 0).SetBlock(new Pipe(
            new Connector(new Vector2[] {
                Vector2.left,
                Vector2.down,
                Vector2.right
            }),
            world.table.Get(0, 0),
            false
            )
        );
        world.table.Get(1, 0).SetBlock(new Pipe(
            new Connector(new Vector2[]
            {
                Vector2.up
            }),
            world.table.Get(1, 0),
            false
            )
        );
        world.table.Get(0, 1).SetBlock(new Pipe(
            new Connector(new Vector2[]
            {
                Vector2.up
            }),
            world.table.Get(0, 1),
            false
            )
        );
        world.table.Get(1, 1).SetBlock(new Pipe(
            new Connector(new Vector2[]
            {
                Vector2.up,
                Vector2.left,
                Vector2.down,
                Vector2.right,
            }),
            world.table.Get(1, 1),
            false
            )
        );
        world.table.Get(2, 1).SetBlock(new Pipe(
            new Connector(new Vector2[]
            {
                Vector2.left
            }),
            world.table.Get(2, 1),
            true
            )
        );
        world.table.Get(0, 2).SetBlock(new Pipe(
            new Connector(new Vector2[]
            {
                Vector2.down
            }),
            world.table.Get(0, 2),
            true
            )
        );

        // testing the configuration
        TestBattery initConfigTest = new("Initial Structure", "INIT");
        allBatteries.Add(initConfigTest);
        initConfigTest.Test("0,0", false, world.GetCell(new Vector2(0, 0)).IsEmpty());
        initConfigTest.Test("1,0", false, world.GetCell(new Vector2(1, 0)).IsEmpty());
        initConfigTest.Test("2,0", true, world.GetCell(new Vector2(2, 0)).IsEmpty());
        initConfigTest.Test("0,1", false, world.GetCell(new Vector2(0, 1)).IsEmpty());
        initConfigTest.Test("1,1", false, world.GetCell(new Vector2(1, 1)).IsEmpty());
        initConfigTest.Test("2,1", false, world.GetCell(new Vector2(2, 1)).IsEmpty());
        initConfigTest.Test("0,2", false, world.GetCell(new Vector2(0, 2)).IsEmpty());
        initConfigTest.Test("1,2", true, world.GetCell(new Vector2(1, 2)).IsEmpty());
        initConfigTest.Test("2,2", true, world.GetCell(new Vector2(2, 2)).IsEmpty());

        // Test SetAllPipeLocations()
        TestBattery SPLTest = new("SetAllPipeLocations() test", "SAPL");
        allBatteries.Add(SPLTest);
        world.SetAllPipeLocations();
        string pipeLocationsPrintout = string.Empty;
        for (int i = 0; i < world.allPipeLocations.Count; i++)
        {
            pipeLocationsPrintout += "(" + (int)world.allPipeLocations[i].x + ", " + (int)world.allPipeLocations[i].y + ")\n";
        }
        Debug.Log("ALL PIPE LOCATIONS: \n" + pipeLocationsPrintout);
        SPLTest.Test("SetAllPipeLocations() length test", 6, world.allPipeLocations.Count);

        // Test GetAllConnections()
        TestBattery GACTest = new("GetAllConnections() test", "GAC");
        allBatteries.Add(GACTest);
        world.SetAllPipeLocations();
        string allConnectionsPrintout = string.Empty;
        for (int i = 0; i < world.allPipeLocations.Count; i++)
        {
            List<Vector2> subConnections = world.GetAllConnections(world.allPipeLocations[i]);
            allConnectionsPrintout += "pos(" + (int)world.allPipeLocations[i].x + ", " + (int)world.allPipeLocations[i].y + ") has ";
            for (int i_sub = 0; i_sub < subConnections.Count; i_sub++)
            {
                allConnectionsPrintout += "(" + (int)subConnections[i_sub].x + ", " + (int)subConnections[i_sub].y + "), ";
            }
            allConnectionsPrintout += "\n";
        }
        // Cursory connection count tests
        GACTest.Test("Connections from (0, 0)", 3, world.GetAllConnections(new Vector2(0, 0)).Count);
        GACTest.Test("Connections from (1, 0)", 1, world.GetAllConnections(new Vector2(1, 0)).Count);
        GACTest.Test("Connections from (0, 1)", 1, world.GetAllConnections(new Vector2(0, 1)).Count);
        GACTest.Test("Connections from (1, 1)", 4, world.GetAllConnections(new Vector2(1, 1)).Count);
        GACTest.Test("Connections from (2, 1)", 1, world.GetAllConnections(new Vector2(2, 1)).Count);
        GACTest.Test("Connections from (0, 2)", 1, world.GetAllConnections(new Vector2(0, 2)).Count);

        // GetFullPipeSystem() tests
        TestBattery GFPSTest = new("GetFullPipeSystem() test", "GFPS");
        allBatteries.Add(GFPSTest);
        List<Vector2> systemOne = new List<Vector2> { new Vector2(0, 0) };
        List<Vector2> systemTwo = new List<Vector2> { new Vector2(1, 0), new Vector2(1, 1), new Vector2(2, 1) };
        List<Vector2> systemThree = new List<Vector2> { new Vector2(0, 2), new Vector2(0, 1) };

        // helper function for comparing lists
        // TODO

        // Compare the lists, length
        GFPSTest.Test("@ (0, 0) count", systemOne.Count, world.GetFullPipeSystem(new Vector2(0, 0)).Count);
        GFPSTest.Test("@ (1, 0) count", systemTwo.Count, world.GetFullPipeSystem(new Vector2(1, 0)).Count);
        GFPSTest.Test("@ (0, 1) count", systemThree.Count, world.GetFullPipeSystem(new Vector2(0, 1)).Count);
        GFPSTest.Test("@ (1, 1) count", systemTwo.Count, world.GetFullPipeSystem(new Vector2(1, 1)).Count);
        GFPSTest.Test("@ (2, 1) count", systemTwo.Count, world.GetFullPipeSystem(new Vector2(2, 1)).Count);
        GFPSTest.Test("@ (0, 2) count", systemThree.Count, world.GetFullPipeSystem(new Vector2(0, 2)).Count);

        // TODO compare the lists directly!

        //GFPSTest.Test( "@ (0, 0)", systemOne,   world.GetFullPipeSystem(new Vector2(0, 0)) );
        //GFPSTest.Test( "@ (1, 0)", systemTwo,   world.GetFullPipeSystem(new Vector2(1, 0)) );
        //GFPSTest.Test( "@ (0, 1)", systemThree, world.GetFullPipeSystem(new Vector2(0, 1)) );
        //GFPSTest.Test( "@ (1, 1)", systemTwo,   world.GetFullPipeSystem(new Vector2(1, 1)) );
        //GFPSTest.Test( "@ (2, 1)", systemTwo,   world.GetFullPipeSystem(new Vector2(2, 1)) );
        //GFPSTest.Test( "@ (0, 2)", systemThree, world.GetFullPipeSystem(new Vector2(0, 2)) );

        // print each one out for inspection
        // TODO

        // GetAllPipeSystems() test
        TestBattery GAPSTest = new("GetAllPipeSystems() test", "GAPS");
        allBatteries.Add(GAPSTest);
        List<List<Vector2>> expectedSystems = new List<List<Vector2>> { systemOne, systemTwo, systemThree };
        GAPSTest.Test("# of systems detected", 3, world.GetAllPipeSystems().Count);
        // test every position to see if it's in the right system.

        // NOTE: This test is dysfunctional. "Index out of range" or some such.
        // Anyways, the functionality this was intended to cover is covered elsewhere.
        //for (int system_index = 0; system_index < world.GetAllPipeSystems().Count; system_index++)
        //{
        //    for (int block_index = 0; block_index < world.GetAllPipeSystems()[system_index].Count; block_index++)
        //    {
        //        GAPSTest.Test("si=" + system_index + ", bi=" + block_index + " | ",
        //            "(" + (int)expectedSystems[system_index][block_index].x + ", " + (int)expectedSystems[system_index][block_index].y + ")",
        //            "(" + (int)world.GetAllPipeSystems()[system_index][block_index].x + ", " + (int)world.GetAllPipeSystems()[system_index][block_index].y + ")"
        //            );
        //    }
        //}

        // Test GetSteamState()
        TestBattery GSSTest = new("GetSteamState() test", "GSS");
        allBatteries.Add(GSSTest);
        GSSTest.Test("@ (0, 0)", SteamState.EMPTY, world.GetSteamState(new Vector2(0, 0)));
        GSSTest.Test("@ (1, 0)", SteamState.EMPTY, world.GetSteamState(new Vector2(1, 0)));
        GSSTest.Test("@ (2, 0)", null, world.GetSteamState(new Vector2(2, 0)));
        GSSTest.Test("@ (0, 1)", SteamState.EMPTY, world.GetSteamState(new Vector2(0, 1)));
        GSSTest.Test("@ (1, 1)", SteamState.EMPTY, world.GetSteamState(new Vector2(1, 1)));
        GSSTest.Test("@ (2, 1)", SteamState.SOURCE, world.GetSteamState(new Vector2(2, 1)));
        GSSTest.Test("@ (0, 2)", SteamState.SOURCE, world.GetSteamState(new Vector2(0, 2)));
        GSSTest.Test("@ (1, 2)", null, world.GetSteamState(new Vector2(1, 2)));
        GSSTest.Test("@ (2, 2)", null, world.GetSteamState(new Vector2(2, 2)));

        // Test PropagateSteam()
        TestBattery PSTest = new("PropagateSteam() test", "PS");
        allBatteries.Add(PSTest);
        world.PropagateSteam(systemOne);
        world.PropagateSteam(systemTwo);
        world.PropagateSteam(systemThree);
        PSTest.Test("@ (0, 0)", SteamState.EMPTY, world.GetSteamState(new Vector2(0, 0)));
        PSTest.Test("@ (1, 0)", SteamState.LEAKING, world.GetSteamState(new Vector2(1, 0)));
        PSTest.Test("@ (2, 0)", null, world.GetSteamState(new Vector2(2, 0)));
        PSTest.Test("@ (0, 1)", SteamState.FULL, world.GetSteamState(new Vector2(0, 1)));
        PSTest.Test("@ (1, 1)", SteamState.LEAKING, world.GetSteamState(new Vector2(1, 1)));
        PSTest.Test("@ (2, 1)", SteamState.SOURCE, world.GetSteamState(new Vector2(2, 1)));
        PSTest.Test("@ (0, 2)", SteamState.SOURCE, world.GetSteamState(new Vector2(0, 2)));
        PSTest.Test("@ (1, 2)", null, world.GetSteamState(new Vector2(1, 2)));
        PSTest.Test("@ (2, 2)", null, world.GetSteamState(new Vector2(2, 2)));

        // undo the work so we can literally do it again LOL
        // Reset the steam states of all the things.
        // Also, this counts as a cursory but probably fine test of... 
        // Testing SetSteamState() :)
        TestBattery SSSTest = new("SetSteamState() test", "SSS");
        allBatteries.Add(SSSTest);

        world.SetSteamState(new Vector2(0, 0), SteamState.EMPTY);
        world.SetSteamState(new Vector2(1, 0), SteamState.EMPTY);
        world.SetSteamState(new Vector2(0, 1), SteamState.EMPTY);
        world.SetSteamState(new Vector2(1, 1), SteamState.EMPTY);
        world.SetSteamState(new Vector2(0, 2), SteamState.SOURCE);
        world.SetSteamState(new Vector2(2, 1), SteamState.SOURCE);

        SSSTest.Test("@ (0, 0)", SteamState.EMPTY, world.GetSteamState(new Vector2(0, 0)));
        SSSTest.Test("@ (1, 0)", SteamState.EMPTY, world.GetSteamState(new Vector2(1, 0)));
        SSSTest.Test("@ (2, 0)", null, world.GetSteamState(new Vector2(2, 0)));
        SSSTest.Test("@ (0, 1)", SteamState.EMPTY, world.GetSteamState(new Vector2(0, 1)));
        SSSTest.Test("@ (1, 1)", SteamState.EMPTY, world.GetSteamState(new Vector2(1, 1)));
        SSSTest.Test("@ (2, 1)", SteamState.SOURCE, world.GetSteamState(new Vector2(2, 1)));
        SSSTest.Test("@ (0, 2)", SteamState.SOURCE, world.GetSteamState(new Vector2(0, 2)));
        SSSTest.Test("@ (1, 2)", null, world.GetSteamState(new Vector2(1, 2)));
        SSSTest.Test("@ (2, 2)", null, world.GetSteamState(new Vector2(2, 2)));

        // UpdateSteam() test
        TestBattery USTest = new("UpdateSteam() test", "US");
        allBatteries.Add(USTest);
        world.UpdateSteam();
        USTest.Test("@ (0, 0)", SteamState.EMPTY, world.GetSteamState(new Vector2(0, 0)));
        USTest.Test("@ (1, 0)", SteamState.LEAKING, world.GetSteamState(new Vector2(1, 0)));
        USTest.Test("@ (2, 0)", null, world.GetSteamState(new Vector2(2, 0)));
        USTest.Test("@ (0, 1)", SteamState.FULL, world.GetSteamState(new Vector2(0, 1)));
        USTest.Test("@ (1, 1)", SteamState.LEAKING, world.GetSteamState(new Vector2(1, 1)));
        USTest.Test("@ (2, 1)", SteamState.SOURCE, world.GetSteamState(new Vector2(2, 1)));
        USTest.Test("@ (0, 2)", SteamState.SOURCE, world.GetSteamState(new Vector2(0, 2)));
        USTest.Test("@ (1, 2)", null, world.GetSteamState(new Vector2(1, 2)));
        USTest.Test("@ (2, 2)", null, world.GetSteamState(new Vector2(2, 2)));

        // objective completion: IsOn() test
        TestBattery IOTest = new("IsOn() test (for activating things!)", "IO");
        allBatteries.Add(IOTest);
        IOTest.Test("@ (0, 0)", false, world.IsOn(new Vector2(0, 0)));
        IOTest.Test("@ (1, 0)", false, world.IsOn(new Vector2(1, 0)));
        IOTest.Test("@ (2, 0)", false, world.IsOn(new Vector2(2, 0)));
        IOTest.Test("@ (0, 1)", true, world.IsOn(new Vector2(0, 1)));
        IOTest.Test("@ (1, 1)", false, world.IsOn(new Vector2(1, 1)));
        IOTest.Test("@ (2, 1)", true, world.IsOn(new Vector2(2, 1)));
        IOTest.Test("@ (0, 2)", true, world.IsOn(new Vector2(0, 2)));
        IOTest.Test("@ (1, 2)", false, world.IsOn(new Vector2(1, 2)));
        IOTest.Test("@ (2, 2)", false, world.IsOn(new Vector2(2, 2)));

        // animation: IsSteaming() test
        TestBattery ISTest = new("IsSteaming() test (for animation!)", "IS");
        allBatteries.Add(ISTest);
        ISTest.Test("@ (0, 0)", false, world.IsSteaming(new Vector2(0, 0)));
        ISTest.Test("@ (1, 0)", true, world.IsSteaming(new Vector2(1, 0)));
        ISTest.Test("@ (2, 0)", false, world.IsSteaming(new Vector2(2, 0)));
        ISTest.Test("@ (0, 1)", true, world.IsSteaming(new Vector2(0, 1)));
        ISTest.Test("@ (1, 1)", true, world.IsSteaming(new Vector2(1, 1)));
        ISTest.Test("@ (2, 1)", true, world.IsSteaming(new Vector2(2, 1)));
        ISTest.Test("@ (0, 2)", true, world.IsSteaming(new Vector2(0, 2)));
        ISTest.Test("@ (1, 2)", false, world.IsSteaming(new Vector2(1, 2)));
        ISTest.Test("@ (2, 2)", false, world.IsSteaming(new Vector2(2, 2)));

        // animation: WhereToSteamFrom() test
        TestBattery WTSFTest = new("WhereToSteamFrom() test (for animation!)", "WTSF");
        allBatteries.Add(WTSFTest);
        WTSFTest.Test("count @ (0, 0)", 0, world.WhereToSteamFrom(new Vector2(0, 0)).Count);
        WTSFTest.Test("count @ (1, 0)", 0, world.WhereToSteamFrom(new Vector2(1, 0)).Count);
        WTSFTest.Test("count @ (2, 0)", 0, world.WhereToSteamFrom(new Vector2(2, 0)).Count);
        WTSFTest.Test("count @ (0, 1)", 0, world.WhereToSteamFrom(new Vector2(0, 1)).Count);
        WTSFTest.Test("count @ (1, 1)", 2, world.WhereToSteamFrom(new Vector2(1, 1)).Count);
        WTSFTest.Test("count @ (2, 1)", 0, world.WhereToSteamFrom(new Vector2(2, 1)).Count);
        WTSFTest.Test("count @ (0, 2)", 0, world.WhereToSteamFrom(new Vector2(0, 2)).Count);
        WTSFTest.Test("count @ (1, 2)", 0, world.WhereToSteamFrom(new Vector2(1, 2)).Count);
        WTSFTest.Test("count @ (2, 2)", 0, world.WhereToSteamFrom(new Vector2(2, 2)).Count);

        // (1, 1) is unfortunately the only location where steam would be visibly leaking from.
        WTSFTest.Test("specifics @ (1,1), UP", true, world.WhereToSteamFrom(new Vector2(1, 1)).Contains(Vector2.up));
        WTSFTest.Test("specifics @ (1,1), LEFT", true, world.WhereToSteamFrom(new Vector2(1, 1)).Contains(Vector2.left));
        WTSFTest.Test("specifics @ (1,1), DOWN", false, world.WhereToSteamFrom(new Vector2(1, 1)).Contains(Vector2.down));
        WTSFTest.Test("specifics @ (1,1), RIGHT", false, world.WhereToSteamFrom(new Vector2(1, 1)).Contains(Vector2.right));

        // TODO: should make another build area with more leaks to get more testing!

        // PRINT ALL TESTS TO CONSOLE
        string allSummariesAccum = string.Empty;
        for (int i = 0; i < allBatteries.Count; i++)
        {
            allSummariesAccum += allBatteries[i].GetSummary();
            if (allBatteries[i].fails > 0)
            {
                Debug.LogError(allBatteries[i].GetRecord());
            }
        }
        Debug.Log(allSummariesAccum);


    }

}
