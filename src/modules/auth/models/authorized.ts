import { ApiProperty } from "@nestjs/swagger";
import { User } from "../../user/models/User";

export class Authorized {

    @ApiProperty()
    accessToken?: string;

    @ApiProperty()
    user?: User

    constructor(accessToken?: string, user?: User) {
        this.user = user;
        this.accessToken = accessToken;
    }
}