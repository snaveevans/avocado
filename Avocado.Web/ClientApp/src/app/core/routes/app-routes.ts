import { AppRoute } from "./app-route";

export enum RouteName {
  Home = "[RouteName] Home",
  Upcoming = "[RouteName] Upcoming",
  MyEvents = "[RouteName] MyEvents",
  Contacts = "[RouteName] Contacts",
  NewEvent = "[RouteName] NewEvent",
  Help = "[RouteName] Help",
  Login = "[RouteName] Login",
  Logout = "[RouteName] Logout"
}

export const allRoutes: { [P in RouteName]: AppRoute } = {
  [RouteName.Home]: new AppRoute("Home", "home", ""),
  [RouteName.Upcoming]: new AppRoute(
    "Upcoming Events",
    "calendar-check",
    "events"
  ),
  [RouteName.MyEvents]: new AppRoute("My Events", "calendar-text", "events"),
  [RouteName.Contacts]: new AppRoute("Contacts", "account-group", "contacts"),
  [RouteName.NewEvent]: new AppRoute("New Event", "plus-circle", "events/new"),
  [RouteName.Help]: new AppRoute("Help", "help-circle", "help"),
  [RouteName.Login]: new AppRoute("Login", "login", "login"),
  [RouteName.Logout]: new AppRoute("Logout", "logout", "logout")
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
