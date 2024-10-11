using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DirectionOps;
using Testing;

public class TEST_Direction : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // initialize each battery
        TestBattery FrIntTs = new("from int, ToDirection()", "Int->", Verbosity.FAILS_ONLY);
        TestBattery ToStrTs = new("ToString()", "->Str", Verbosity.FAILS_ONLY);
        TestBattery ToIdTs = new("ToId()", "->Id", Verbosity.FAILS_ONLY);
        TestBattery ToAngTs = new("ToAngle()", "->Ang", Verbosity.FAILS_ONLY);
        TestBattery ToVecTs = new("ToVector()", "->Vec", Verbosity.FAILS_ONLY);
        TestBattery AddTs = new("Add()", "Add", Verbosity.FAILS_ONLY);
        TestBattery ARotTs = new("ApplyRotation()", "Rot", Verbosity.FAILS_ONLY);

        // all test batteries
        TestBattery[] allBatteries = {
            FrIntTs,
            ToStrTs,
            ToIdTs,
            ToAngTs,
            ToVecTs,
            AddTs,
            ARotTs
        };

        // DO THE TESTS

        // STABLE CONVERSIONS
        FrIntTs.Test("up", Direction.UP, (-8).ToDirection());
        FrIntTs.Test("left", Direction.LEFT, (-7).ToDirection());
        FrIntTs.Test("down", Direction.DOWN, (-6).ToDirection());
        FrIntTs.Test("right", Direction.RIGHT, (-5).ToDirection());
        FrIntTs.Test("up", Direction.UP, (-4).ToDirection());
        FrIntTs.Test("left", Direction.LEFT, (-3).ToDirection());
        FrIntTs.Test("down", Direction.DOWN, (-2).ToDirection());
        FrIntTs.Test("right", Direction.RIGHT, (-1).ToDirection());
        FrIntTs.Test("up", Direction.UP, 0.ToDirection());
        FrIntTs.Test("left", Direction.LEFT, 1.ToDirection());
        FrIntTs.Test("down", Direction.DOWN, 2.ToDirection());
        FrIntTs.Test("right", Direction.RIGHT, 3.ToDirection());
        FrIntTs.Test("up", Direction.UP, 4.ToDirection());
        FrIntTs.Test("left", Direction.LEFT, 5.ToDirection());
        FrIntTs.Test("down", Direction.DOWN, 6.ToDirection());
        FrIntTs.Test("right", Direction.RIGHT, 7.ToDirection());
        FrIntTs.Test("up", Direction.UP, 8.ToDirection());

        ToStrTs.Test("up", "UP", Direction.UP.ToString());
        ToStrTs.Test("left", "LEFT", Direction.LEFT.ToString());
        ToStrTs.Test("down", "DOWN", Direction.DOWN.ToString());
        ToStrTs.Test("right", "RIGHT", Direction.RIGHT.ToString());

        ToIdTs.Test("up", 0, Direction.UP.ToId());
        ToIdTs.Test("left", 1, Direction.LEFT.ToId());
        ToIdTs.Test("down", 2, Direction.DOWN.ToId());
        ToIdTs.Test("right", 3, Direction.RIGHT.ToId());

        float DELTA = 0.0000001f;
        ToAngTs.TestDelta("up", 0.0f, Direction.UP.ToAngle(), DELTA);
        ToAngTs.TestDelta("left", 270.0f, Direction.LEFT.ToAngle(), DELTA);
        ToAngTs.TestDelta("down", 180.0f, Direction.DOWN.ToAngle(), DELTA);
        ToAngTs.TestDelta("right", 90.0f, Direction.RIGHT.ToAngle(), DELTA);

        ToVecTs.Test("up", Vector2.up, Direction.UP.ToVector());
        ToVecTs.Test("left", Vector2.left, Direction.LEFT.ToVector());
        ToVecTs.Test("down", Vector2.down, Direction.DOWN.ToVector());
        ToVecTs.Test("right", Vector2.right, Direction.RIGHT.ToVector());

        // ADDITION
        AddTs.Test("UP+up", Direction.UP, Direction.UP.Add(Direction.UP));
        AddTs.Test("UP+left", Direction.LEFT, Direction.UP.Add(Direction.LEFT));
        AddTs.Test("UP+down", Direction.DOWN, Direction.UP.Add(Direction.DOWN));
        AddTs.Test("UP+right", Direction.RIGHT, Direction.UP.Add(Direction.RIGHT));
        AddTs.Test("LEFT+up", Direction.LEFT, Direction.LEFT.Add(Direction.UP));
        AddTs.Test("LEFT+left", Direction.DOWN, Direction.LEFT.Add(Direction.LEFT));
        AddTs.Test("LEFT+down", Direction.RIGHT, Direction.LEFT.Add(Direction.DOWN));
        AddTs.Test("LEFT+right", Direction.UP, Direction.LEFT.Add(Direction.RIGHT));
        AddTs.Test("DOWN+up", Direction.DOWN, Direction.DOWN.Add(Direction.UP));
        AddTs.Test("DOWN+left", Direction.RIGHT, Direction.DOWN.Add(Direction.LEFT));
        AddTs.Test("DOWN+down", Direction.UP, Direction.DOWN.Add(Direction.DOWN));
        AddTs.Test("DOWN+right", Direction.LEFT, Direction.DOWN.Add(Direction.RIGHT));
        AddTs.Test("RIGHT+up", Direction.RIGHT, Direction.RIGHT.Add(Direction.UP));
        AddTs.Test("RIGHT+left", Direction.UP, Direction.RIGHT.Add(Direction.LEFT));
        AddTs.Test("RIGHT+down", Direction.LEFT, Direction.RIGHT.Add(Direction.DOWN));
        AddTs.Test("RIGHT+right", Direction.DOWN, Direction.RIGHT.Add(Direction.RIGHT));

        // ROTATION
        // TODO perform a more comprehensive test!
        ARotTs.Test("up_1", new Vector2(2,3), Direction.UP.ApplyRotation(new Vector2(2,3)) );
        ARotTs.Test("left_1", new Vector2(-3, 2), Direction.LEFT.ApplyRotation(new Vector2(2, 3)) );
        ARotTs.Test("down_1", new Vector2(-2,-3), Direction.DOWN.ApplyRotation(new Vector2(2,3)) );
        ARotTs.Test("right_1", new Vector2(3,-2), Direction.RIGHT.ApplyRotation(new Vector2(2,3)) );
        
        // Read out the summaries of each test.
        string fullSummary = "All tests:\n";
        foreach (TestBattery tb in allBatteries)
        {
            fullSummary += tb.GetSummary();
        }
        Debug.Log(fullSummary);
    
    }

    
}
