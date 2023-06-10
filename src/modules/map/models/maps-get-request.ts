import { MapField } from '../map.types';
import { ApiProperty } from "@nestjs/swagger";

export class MapsGetRequest {

  @ApiProperty()
  filters?: {
    fields?: Array<{ value: string; field: MapField; }>

    userIds?: number[];

    tagIds?: number[];

    isEmpty?: boolean;

    isComplete?: boolean;

    isFavorite?: boolean;
  }

  @ApiProperty()
  sort?: {
    field: MapField;
  }

  @ApiProperty()
  take?: number;

  @ApiProperty()
  skip?: number
}
