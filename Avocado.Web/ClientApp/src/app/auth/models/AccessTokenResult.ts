import { AuthResult } from "@avocado/auth/models/AuthResult";

export class AccessTokenResult {
  constructor(public authResult: AuthResult, public accessToken?: string) {}
}
