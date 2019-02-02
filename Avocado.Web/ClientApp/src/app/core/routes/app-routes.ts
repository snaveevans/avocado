import { AppRoute } from "./app-route";

enum RouteName {
  Home = "[RouteName] Home",
  Upcoming = "[RouteName] upcoming",
  MyEvents = "[RouteName] myEvents",
  Contacts = "[RouteName] contacts",
  NewEvent = "[RouteName] newEvent",
  Help = "[RouteName] help"
}

const allRoutes: { [P in RouteName]: AppRoute } = {
  [RouteName.Home]: new AppRoute("Home", "home", ""),
  [RouteName.Upcoming]: new AppRoute(
    "Upcoming Events",
    "event_available",
    "events"
  ),
  [RouteName.MyEvents]: new AppRoute("My Events", "events", "events"),
  [RouteName.Contacts]: new AppRoute("Contacts", "person", "contacts"),
  [RouteName.NewEvent]: new AppRoute(
    "New Event",
    "add_circle_outline",
    "events/new"
  ),
  [RouteName.Help]: new AppRoute("Help", "help_outline", "help")
};

export const menuRoutes: RouteName[] = [...Object.values(RouteName)];

export const bottomRoutes: RouteName[] = [
  RouteName.Upcoming,
  RouteName.MyEvents,
  RouteName.Contacts
];

export const getRoutes = (routeNames: RouteName[]) => {
  return routeNames.map(rn => allRoutes[rn]);
};
