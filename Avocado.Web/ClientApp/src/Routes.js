import Home from './components/Home';
import EventList from "./components/EventList";
import Login from "./components/Login";

export const routes = {
    home: {
        path: "/",
        component: Home,
        exact: true
    },
    event: {
        path: "/events",
        component: EventList
    },
    login: {
        path: "/login",
        component: Login
    }
};