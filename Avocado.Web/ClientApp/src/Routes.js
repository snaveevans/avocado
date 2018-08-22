import Home from './components/Home';
import EventList from "./components/EventList";
import Login from "./components/Login";

export const home = {
    path: "/",
    component: Home,
    exact: true,
    text: "Home"
}

export const events = {
    path: "/events",
    component: EventList,
    text: "Events"
}

export const login = {
    path: "/login",
    component: Login,
    text: "Login"
}