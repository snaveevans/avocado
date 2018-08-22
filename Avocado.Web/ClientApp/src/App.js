import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout/Layout';
import * as Routes from "./Routes";

export default () => (
  <Layout>
    {
      Object.keys(Routes).map(key => {
        const route = Routes[key];
        return <Route key={key} 
          exact={route.exact} 
          path={route.path} 
          component={route.component} />
      })
    }
  </Layout>
);
