import jwtDecodeToken from "jwt-decode";

const decodeToken = (token: string): { [key: string]: any } => {
  return jwtDecodeToken(token);
};

export class JwtData {
  public readonly id = "";
  public readonly name = "";
  public readonly token: string = "";
  public readonly expires: Date;
  constructor(token: string) {
    const decoded = decodeToken(token);
    this.id = decoded.jti;
    this.name = decoded.sub;
    this.token = token;
    this.expires = new Date(decoded.exp * 1000);
  }
  isExpired(): boolean {
    return this.expires !== undefined && this.expires <= new Date();
  }
}
