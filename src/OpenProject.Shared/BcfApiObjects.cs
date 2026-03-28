using System;
using System.Collections.Generic;

namespace iabi.BCF.APIObjects.V21
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public class Direction
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public class Location
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public class Clipping_plane
    {
        public Location Location { get; set; }
        public Direction Direction { get; set; }
    }

    public class Perspective_camera
    {
        public Point Camera_view_point { get; set; }
        public Direction Camera_direction { get; set; }
        public Direction Camera_up_vector { get; set; }
        public double Field_of_view { get; set; }
    }

    public class Orthogonal_camera
    {
        public Point Camera_view_point { get; set; }
        public Direction Camera_direction { get; set; }
        public Direction Camera_up_vector { get; set; }
        public double View_to_world_scale { get; set; }
    }

    public class Viewpoint_GET
    {
        public string Guid { get; set; }
        public Components Components { get; set; }
        public Perspective_camera Perspective_camera { get; set; }
        public Orthogonal_camera Orthogonal_camera { get; set; }
        public List<Clipping_plane> Clipping_planes { get; set; }
    }

    public class Components
    {
        public List<Component> Selection { get; set; }
        public Visibility Visibility { get; set; }
    }

    public class Component
    {
        public string Ifc_guid { get; set; }
        public string Originating_system { get; set; }
        public string Authoring_tool_id { get; set; }
    }

    public class Visibility
    {
        public bool Default_visibility { get; set; }
        public List<Component> Exceptions { get; set; }
        public List<View_setup_hints> View_setup_hints { get; set; }
    }

    public class View_setup_hints
    {
        public bool Spaces { get; set; }
        public bool Openings { get; set; }
    }

    public class Viewpoint_POST
    {
        public string Guid { get; set; }
        public Components Components { get; set; }
        public Perspective_camera Perspective_camera { get; set; }
        public Orthogonal_camera Orthogonal_camera { get; set; }
        public List<Clipping_plane> Clipping_planes { get; set; }
        public Snapshot_POST Snapshot { get; set; }
    }

    public class Snapshot_POST
    {
        public Snapshot_type Snapshot_type { get; set; }
        public string Snapshot_data { get; set; }
    }

    public enum Snapshot_type
    {
        Png,
        Jpg
    }
}
