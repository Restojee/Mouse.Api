import { User } from "../../user/models/User";
import { ApiProperty } from "@nestjs/swagger";
import {
  Exclude,
  Expose
} from "class-transformer";

@Exclude()
export class Map {

  @Expose()
  @ApiProperty()
  id?: number;

  @Expose()
  @ApiProperty()
  name?: string;

  @Expose()
  @ApiProperty()
  description?: string;

  @Expose()
  @ApiProperty()
  image?: string;

  @Expose()
  @ApiProperty()
  user?: User;

  @Expose()
  @ApiProperty()
  createdAt?: Date;

  @Expose()
  @ApiProperty()
  updatedAt?: Date;
}
