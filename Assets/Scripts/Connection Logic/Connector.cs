using System;
using UnityEngine;


public class Connector
{
    /// <summary>
    /// The block object this connector represents.
    /// </summary>
    private Block block;
    /// <summary>
    /// Coordinates within the build area. Each coordinate should be an integer. 
    /// </summary>
    private Vector2 location;
    /// <summary>
    /// Array of relative coordinates that this connector will try to connect to. Each vector should be an orthogonal unit vector, i.e., Vector2(-1, 0).
    /// </summary>
    private Vector2[] allConnections;
    /// <summary>
    /// The build area this connector is located in.
    /// </summary>
    private BuildArea buildArea;

/*    /// <summary>
    /// As a standard,the left-most connection on the connector is facing.
    /// </summary>
    private float direction;*/

    /// <summary>
    /// constructor overload. See main constructor for details.
    /// </summary>
    /// <param name="block">Block obj this connector represents.</param>
    /// <param name="location">Coordinate location within the build area. Should be an integer vector.</param>
    /// <param name="allConnections">Relative locations this connector will try to connect to. This is intended to be an orthogonal unit vector.</param>
    /// <param name="buildArea">The build area that this connector is a part of.</param>
/*    /// <param name="angle"></param>*/
    public Connector(Block block, Vector2 location, Vector2[] allConnections, BuildArea buildArea, Direction angle)
    {
        Connector(block, location, allConnections, buildArea, (float)angle);
    }
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="block"></param>
    /// <param name="location"></param>
    /// <param name="allConnections"></param>
    /// <param name="buildArea"></param>
    /// <param name="angle"></param>
    public Connector(Block block, Vector2 location, Vector2[] allConnections, BuildArea buildArea, float angle)
	{

	}
}
