using System;

namespace Eva.Layout
{
  public class Occupation
  {
    public Occupation(Location location, Person person)
    {
      Location = location;
      Person = person;
    }

    public Location Location { get; }

    public Person Person { get; }
  }
}
