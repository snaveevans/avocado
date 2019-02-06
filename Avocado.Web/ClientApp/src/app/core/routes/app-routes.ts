import { AppRoute } from "./app-route";
import {
  faSignInAlt,
  faSignOutAlt,
  faQuestionCircle,
  faPlusCircle,
  faUsers,
  faCalendarCheck,
  faCalendarAlt,
  faHome
} from "@fortawesome/free-solid-svg-icons";

enum RouteName {
  Home = "[RouteName] Home",
  Upcoming = "[RouteName] Upcoming",
  MyEvents = "[RouteName] MyEvents",
  Contacts = "[RouteName] Contacts",
  NewEvent = "[RouteName] NewEvent",
  Help = "[RouteName] Help",
  Login = "[RouteName] Login",
  Logout = "[RouteName] Logout"
}

const allRoutes: { [P in RouteName]: AppRoute } = {
  [RouteName.Home]: new AppRoute("Home", faHome, ""),
  [RouteName.Upcoming]: new AppRoute(
    "Upcoming Events",
    faCalendarCheck,
    "events"
  ),
  [RouteName.MyEvents]: new AppRoute("My Events", faCalendarAlt, "events"),
  [RouteName.Contacts]: new AppRoute("Contacts", faUsers, "contacts"),
  [RouteName.NewEvent]: new AppRoute("New Event", faPlusCircle, "events/new"),
  [RouteName.Help]: new AppRoute("Help", faQuestionCircle, "help"),
  [RouteName.Login]: new AppRoute("Login", faSignInAlt, "login"),
  [RouteName.Logout]: new AppRoute("Logout", faSignOutAlt, "login", {
    logout: true
  })
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
