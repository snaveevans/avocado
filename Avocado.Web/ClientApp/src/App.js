import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout/Layout';
import { routes } from "./Routes";

export default () => (
  <Layout>
    {
      Object.keys(routes).map(key => {
        const route = routes[key];
        return <Route key={key} 
          exact={route.exact} 
          path={route.path} 
          component={route.component} />
      })
    }
  </Layout>
);
